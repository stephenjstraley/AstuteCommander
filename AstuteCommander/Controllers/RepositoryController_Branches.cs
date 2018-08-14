using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public IActionResult RepoBranches(string id)
        {
            ViewBag.GUID = id;

            return PartialView();
        }

        public IActionResult GetRepoBranches([DataSourceRequest]DataSourceRequest request, string id)
        {
            var r = GetRepositoryBranchesAndStats(id);
            if (r == null)
            {
//                var asd = "Stop here";
            }
            var startingFolder = string.Empty;
            var repoName = string.Empty;
            var currentBranch = string.Empty;

            try
            {
                CreateStartingFolder(ref startingFolder);

                // See or create sub-folder based on PROJECT name

                if (System.IO.Directory.Exists(startingFolder))
                {
                    CreateStartingProjectFolder(ref startingFolder);

                    var mainBat = startingFolder + @"\WhichBranch.bat";

                    if (System.IO.File.Exists(mainBat))
                        System.IO.File.Delete(mainBat);

                    var bat = new Classes.GitBatch(mainBat)
                    {
                        Redirect = true,
                        RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\ErrorWB.txt",
                        RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\OutputFoo.txt"
                    };

                    var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                    var repo = vsts.GetRepositoryOnGuid(id);

                    repoName = repo.Name;

                    int ndx = repo.RemoteUrl.LastIndexOf(@"/");
                    string theFile = repo.RemoteUrl.Substring(ndx + 1);

                    bat.AddLine($"echo Working on '{theFile}'");
                    bat.AddCD(startingFolder);
                    bat.AddLine($@"git clone {repo.RemoteUrl}");
                    bat.AddCD($@"{startingFolder}\{theFile}");
                    bat.AddLine("git branch");

                    bat.Write();
                    bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                    // No error so parse file
                    var lines = System.IO.File.ReadAllLines($@"{startingFolder.Replace(@"\\", @"\")}\OutputFoo.txt");
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("*"))
                        {
                            currentBranch = line.Substring(2);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                currentBranch = string.Empty;
            }


            List<RepoBranchVM> branches = new List<RepoBranchVM>();

            foreach (var b in r.Branches)
            {
                var item = new RepoBranchVM()
                {
                    Statuses = new List<RepoBranchStatusVM>(),
                    RepoGuid = id,
                    Guid = b.Guid,
                    Name = b.Name.Replace("refs/heads/", string.Empty),
                    Url = b.Url,
                    Category = b.Name.Contains(@"tags/") ? "TAG" : b.Name.Contains(@"pull/") ? "PULL" : "BRANCH",
                    IsLocked = false,
                    IsCurrentBranch = false,
                    LockedBy = string.Empty
                };
                if (!string.IsNullOrEmpty(b.IsLocked))
                {
                    item.IsLocked = b.IsLocked.ToUpper() == "TRUE";
                    item.LockedBy = b.Locked.Email;
                }
                item.HasStats = (b.Statuses.Count != 0);

                if (!string.IsNullOrEmpty(currentBranch))
                {
                    if (item.Name.ToUpper() == currentBranch.ToUpper())
                    {
                        item.IsCurrentBranch = true;
                    }
                }
                foreach (var s in b.Statuses)
                {
                    var stat = new RepoBranchStatusVM()
                    {
                        CreatedBy = s.CreatedBy.By,
                        CreatedDate = System.Convert.ToDateTime(s.CreationDate).ToShortDateString(),
                        Description = s.Description,
                        Id = s.Id,
                        State = s.State
                    };
                    item.Statuses.Add(stat);
                }

                branches.Add(item);
            }

            return Json(branches.ToDataSourceResult(request));

        }

        public JsonResult CheckOutBranch(string name, string guid)
        {
            var startingFolder = string.Empty;
            var repoName = string.Empty;
            name = name.Replace("refs/heads/", string.Empty);

            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            var selectedRepo = vsts.GetRepositoryOnGuid(guid);

            repoName = selectedRepo.Name;

            try
            {
                CreateStartingFolder(ref startingFolder);

                if (System.IO.Directory.Exists(startingFolder))
                {
                    CreateStartingProjectFolder(ref startingFolder);

                    var mainBat = startingFolder + @"\CheckOutBranch.bat";

                    if (System.IO.File.Exists(mainBat))
                        System.IO.File.Delete(mainBat);

                    var bat = new Classes.GitBatch(mainBat)
                    {
                        Redirect = true,
                        RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\Error.txt",
                        RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\output.txt"
                    };

                    bat.AddLine($"echo start");

                    bat.AddLine($"echo Working on '{name}'");

                    if (System.IO.Directory.Exists($"{startingFolder}\\{repoName}"))
                    {
                        bat.AddCD($@"{startingFolder}\{repoName}");
                        bat.AddReset();
                        bat.AddCheckout(name, $"origin/{name}");
                        bat.AddCD(startingFolder);

                        bat.Write();

                        bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                        return Json(new JsonMessage(true, name + " : Pulled" ));
                    }
                    else
                        return Json(new JsonMessage(false, " : FAILED - you need to clone repository first" ));
                }
                else
                {
                    return Json(new JsonMessage(false, " : FAILED - you need to clone repository first" ));
                }

            }
            catch (System.Exception ex)
            {
                return Json(new JsonMessage(false, $"{repoName} - Failed to check out - [{ex.Message}]" ));
            }

        }
        public JsonResult DeleteBranch([DataSourceRequest] DataSourceRequest dsRequest, RepoBranchVM branch)
        {
            if (branch != null && ModelState.IsValid)
            {
                var branchPath = branch.Name.Split("/");
                var canContinue = true;

                foreach (var bp in branchPath)
                {
                    if (bp == "release" || bp == "master" || bp == "develop" || bp == "tags")
                    {
                        canContinue = false;
                        break;
                    }
                }

                if (!canContinue)
                {
                    return Json(new JsonMessage(true, "Cannot delete this branch" ));
                }
                else
                {
                    var startingFolder = string.Empty;
                    CreateStartingFolder(ref startingFolder);

                    if (System.IO.Directory.Exists(startingFolder))
                    {
                        CreateStartingProjectFolder(ref startingFolder);

                        if (System.IO.Directory.Exists(startingFolder))
                        {
                            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                            var selectedRepo = vsts.GetRepositoryOnGuid(branch.RepoGuid);

                            var repoName = selectedRepo.Name;

                            var mainBat = startingFolder + @"\DeleteFeatureBranch.bat";

                            if (System.IO.File.Exists(mainBat))
                                System.IO.File.Delete(mainBat);

                            var bat = new Classes.GitBatch(mainBat)
                            {
                                Redirect = true,
                                RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\ErrorDelete.txt",
                                RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\OutputDelete.txt"
                            };

                            bat.AddLine($"echo start");

                            var newName = branch.Name.Replace("refs/heads/", string.Empty);

                            bat.AddCD($@"{startingFolder}\{repoName}");
                            bat.AddReset();
                            bat.AddCheckout("master", "master");                        
                            bat.AddLine($@"git branch -D {newName}");  // Remove from local
                            bat.AddLine($@"git push origin --delete {newName}");  // Remove from origin

                            bat.AddCD(startingFolder);

                            bat.Write();

                            bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                            return Json(new JsonMessage(true, "Branch Deleted" ));
                        }
                        else
                            return Json(new JsonMessage(true, $"Unable to find starting folder {startingFolder}" ));

                    }
                    else
                        return Json(new JsonMessage(true, $"Unable to find starting folder {startingFolder}" ));
                }
            }
            else
                return Json(new JsonMessage(true, "Invalid Model State"));
        }
        public JsonResult MakeFeatureBranch([DataSourceRequest] DataSourceRequest dsRequest, RepositoryVM repo)
        {
            try
            {
                if (repo != null && ModelState.IsValid)
                {
                    // get work itm
                    Classes.VSTS.VstsWorkItem pbi = GetWorkItem(repo.PBI);

                    if (pbi != null)
                    {
                        if (pbi.StatusCode != System.Net.HttpStatusCode.OK)
                            return Json(new { result = false, message = "Invalid Work Item" });
                        else
                        {
                            string branchName = string.Empty;

                            #region Set Branch Name to start
                            if (!string.IsNullOrEmpty(repo.OverridingName)) // We use this name
                                branchName = repo.OverridingName.Replace(" ", "_");
                            else
                                branchName = pbi.Fields.Title;
                            #endregion

                            branchName = pbi.Id + "_" + ClearBatchName(branchName);

                            // Clear model so it will be considered "dirty" if called again
                            repo.PBI = string.Empty;
                            repo.OverridingName = string.Empty;

                            #region  Write Bat File
                            var startingFolder = string.Empty;
                            CreateStartingFolder(ref startingFolder);
                            if (System.IO.Directory.Exists(startingFolder))
                            {
                                CreateStartingProjectFolder(ref startingFolder);

                                if (System.IO.Directory.Exists(startingFolder))
                                {
                                    var bat = WriteFeatureBranchGit(repo, startingFolder, branchName);

                                    try
                                    {
                                        bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                                        return Json(new JsonMessage(true, "Feature Branch Created"));
                                    }
                                    catch (System.Exception ex)
                                    {
                                        return Json(new JsonMessage(false, ex.Message));
                                    }
                                }
                                else
                                    return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}"));
                            }
                            else
                                return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}"));

                            #endregion
                        }
                    }
                    else   // create branch based on name
                    {
                        string branchName = string.Empty;
                        if (!string.IsNullOrEmpty(repo.OverridingName)) // We use this name
                            branchName = repo.OverridingName.Replace(" ", "_");
                        else
                            return Json(new JsonMessage(false, "Empty branch name"));

                        branchName = ClearBatchName(branchName);

                        repo.PBI = string.Empty;
                        repo.OverridingName = string.Empty;

                        #region  Write Bat File
                        var startingFolder = string.Empty;
                        CreateStartingFolder(ref startingFolder);
                        if (System.IO.Directory.Exists(startingFolder))
                        {
                            CreateStartingProjectFolder(ref startingFolder);

                            if (System.IO.Directory.Exists(startingFolder))
                            {
                                var bat = WriteFeatureBranchGit(repo, startingFolder, branchName);

                                try
                                {
                                    bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                                    return Json(new JsonMessage(true, "Feature Branch Created"));
                                }
                                catch (System.Exception ex)
                                {
                                    return Json(new JsonMessage(false, ex.Message));
                                }
                            }
                            else
                                return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}"));
                        }
                        else
                            return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}"));

                        #endregion
                    }
                }
                else
                    return Json(new JsonMessage(false, "Invalid Model State" ));
            }
            catch (System.Exception ex)
            {
                return Json(new JsonMessage(false, ex.Message ));
            }
        }

        protected string ClearBatchName(string name)
        {
            while (name.Contains(":"))
            {
                int nPos = name.IndexOf(":");
                name = name.Substring(nPos + 1).Trim();
            }

            name = name.RemoveBetween("[]").RemoveBetween("()").Trim();

            var rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_ ]");
            name = rgx.Replace(name, string.Empty).ToLower();

            while (name.Contains("  "))
                name = name.Replace("  ", " ");

            name = name.Replace("-", string.Empty).Replace(" ", "_");
            name = name.Replace("__", "_");

            return name;
        }

        protected Classes.GitBatch WriteFeatureBranchGit(RepositoryVM repo, string startingFolder, string branchName)
        {
            var mainBat = startingFolder + @"\PullForFeatureBranch.bat";

            if (System.IO.File.Exists(mainBat))
                System.IO.File.Delete(mainBat);

            // see if folder for REPO is there and if not, pull it - if it is recheck out master and release
            var bat = new Classes.GitBatch(mainBat)
            {
                Redirect = true,
                RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\ErrorFB.txt",
                RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\OutputFB.txt"
            };

            bat.AddLine($"echo start");

            var theFile = repo.Name;
            var featurePrefix = _settings.Setting.GetSection("FeatureBranchPrefix").Value ?? "feature/";
            var releasePrefix = _settings.Setting.GetSection("ReleaseBranchName").Value ?? "release";
            var masterPrefix = _settings.Setting.GetSection("MasterBranchName").Value ?? "master";

            var featureFile = $"{featurePrefix}{branchName}";
            if (featureFile.Length > 60)
                featureFile = featureFile.Substring(0, 60);

            bat.AddLine($"echo Working on '{theFile}'");

            bat.AddCD($@"{startingFolder}\{theFile}");
            bat.AddReset();

            if (repo.OffRelease)
            {
                bat.AddCheckout(releasePrefix, $"origin/{releasePrefix}");
                bat.AddLine($"git checkout -b {featureFile} {releasePrefix}");
            }
            else
            {
                bat.AddCheckout(masterPrefix, $"origin/{masterPrefix}");
                bat.AddLine($"git checkout -b {featureFile} {masterPrefix}");
            }

            bat.AddLine($"git push -u origin {featureFile}");

            bat.AddCD(startingFolder);

            bat.Write();

            return bat;
        }

    }

}
