using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace AstuteCommander.Controllers
{
   
    public partial class RepositoryController : AstuteController
    {
        static bool _inBuildProcess = false;
        static bool _inReleaseProcess = false;

        ILogger<RepositoryController> _logger;

        public RepositoryController(ILogger<RepositoryController> logger,
                                    IMemoryCache memCache, 
                                    IAppSettings settings, 
                                    IHostingEnvironment env) : base(memCache, settings, env)
        {
            Classes.Raz.root = env.WebRootPath;
            _logger = logger;
        }

        public IActionResult FoundProject([DataSourceRequest]DataSourceRequest request, string project)
        {
            _logger.LogInformation("This is a test");

            ViewBag.Project = project;

            HttpContext.Session.SetString(Session.SELECTED_PROJ, project);
            HttpContext.Session.SetString(Session.HEARTBEAT, System.DateTime.Now.ToString());

            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));

            ViewBag.RepoCount = vsts.GetRepos(project).Count;

            Task.Run(() => SetBuildDefinitions(project));
            Task.Run(() => SetReleaseDefinitions(project));

            return View("ProjectRepositories");
        }

        public IActionResult GetRepos([DataSourceRequest]DataSourceRequest request, string projectName)
        {
            var startingFolder = string.Empty;
            List<RepositoryVM> results = new List<RepositoryVM>();

            try
            {
                CreateStartingFolder(ref startingFolder);

                var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));


                var repos = vsts.GetRepos(projectName);
                var items = repos.Repositories.OrderBy(t => t.Name).ToList();
                Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 1 }, item =>
                {
                    var toAdd = new RepositoryVM()
                    {
                        Guid = item.Guid,
                        Name = item.Name,
                        Url = item.RemoteUrl,
                        DefaultBranch = item.DefaultBranch,
                        DirectoryOnDisk = false
                    };
                    if (System.IO.Directory.Exists(startingFolder))
                    {
                        var projectStartingFolder = startingFolder;
                        CreateStartingProjectFolder(ref projectStartingFolder);
                        if (System.IO.Directory.Exists(startingFolder))
                        {
                            toAdd.DirectoryOnDisk = System.IO.Directory.Exists(projectStartingFolder + @"\" + item.Name);
                        }
                    }
                    results.Add(toAdd);
                });
            }
            catch (System.Exception foo)
            {
                System.Diagnostics.Debug.WriteLine($"Get Repos: {foo.Message}");
            }
            return Json(results.ToDataSourceResult(request));
        }
        protected void SetBuildDefinitions(string name)
        {
            Classes.VSTS.VstsBuildDefinitions defs;

            try
            {
                if (!_inBuildProcess)
                {
                    if (!_cache.TryGetValue(CACHE.BUILDS, out defs))
                    {
                        _inBuildProcess = true;

                        var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);

                        var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                        var temp = vsts.GetDetiledBuildDefinitions(name);

                        _cache.Set(CACHE.BUILDS, temp, options);

                        _inBuildProcess = false;
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Builds -> " + e.Message);
            }
//            _cache.TryGetValue(CACHE.BUILDS, out defs);
//            var x = defs;
        }
        protected void SetReleaseDefinitions(string name)
        {
            Classes.VSTS.VstsReleaseDefinitions defs = null;

            try
            {
                if (!_inReleaseProcess)
                {
                    if (!_cache.TryGetValue(CACHE.RELEASES, out defs))
                    {
                        _inReleaseProcess = true;

                        var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);

                        var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                        var temp = vsts.GetDetailedReleaseDefinitions(name);

                        _cache.Set(CACHE.RELEASES, temp, options);

                        _inReleaseProcess = false;
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Releases -> " + e.Message);
            }
        }
        public IActionResult RebuildBuildDefinitions()
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));

            ViewBag.RepoCount = vsts.GetRepos(HttpContext.Session.GetString(Session.SELECTED_PROJ)).Count;

            try
            {
                if (!_inBuildProcess)
                {
                    var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
                    _cache.Remove(CACHE.BUILDS);
                    ConstructBuildDefinitions();
//                    var background = Task.Run(() => SetBuildDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ)));
                }
            }
            catch (System.Exception exx)
            {
                System.Diagnostics.Debug.WriteLine($"Rebuild Build Definitions:  {exx.Message}");
            }
            return View("ProjectRepositories");
        }
        public async void ConstructBuildDefinitions()
        {
//            SetBuildDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ));
//            var background =
                await Task.Run(() => SetBuildDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ)));
        }
        public IActionResult RebuildReleaseDefinitions()
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));

            ViewBag.RepoCount = vsts.GetRepos(HttpContext.Session.GetString(Session.SELECTED_PROJ)).Count;

            if (!_inReleaseProcess)
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
                _cache.Remove(CACHE.RELEASES);
                ConstructReleaseDefinitions();
                //                Task.Run(() => SetReleaseDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ)));
            }
            return View("ProjectRepositories");
        }
        public async void ConstructReleaseDefinitions()
        {
            //            SetBuildDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ));
            //            var background =
            await Task.Run(() => SetReleaseDefinitions(HttpContext.Session.GetString(Session.SELECTED_PROJ)));
        }
        public JsonResult PullAllRepositories(int pos)
        {
            var repoName = string.Empty;

            try
            {
                // get starting folder from login
                var startingFolder = string.Empty;

                CreateStartingFolder(ref startingFolder);

                if (System.IO.Directory.Exists(startingFolder))
                {
                    CreateStartingProjectFolder(ref startingFolder);

                    if (System.IO.Directory.Exists(startingFolder))
                    {
                        var mainBat = startingFolder + @"\PullAllRepositories.bat";

                        if (System.IO.File.Exists(mainBat))
                            System.IO.File.Delete(mainBat);

                        var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                        var repos = vsts.GetRepos(HttpContext.Session.GetString(Session.SELECTED_PROJ));

                        var repo = repos.Repositories[pos];

                        repoName = repo.Name;

                        int ndx = repo.RemoteUrl.LastIndexOf(@"/");
                        string theFile = repo.RemoteUrl.Substring(ndx + 1);

                        if (System.IO.Directory.Exists($@"{startingFolder}\{theFile}"))
                            return Json(new JsonMessage(true, "Already exists"));
                        else
                        {
                            var bat = new Classes.GitBatch(mainBat)
                            {
                                Redirect = true,
                                RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\ErrorPullAll{pos}.txt",
                                RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\OutputPullAll{pos}.txt"
                            };

                            bat.AddLine($"echo start");

                            bat.AddLine($"echo Working on '{theFile}'");
                            bat.AddCD(startingFolder);
                            bat.AddLine($@"git clone {repo.RemoteUrl}");

                            bat.AddCD($@"{startingFolder}\{theFile}");
                            //bat.AddLine($"git checkout -b master origin/master");
                            //bat.AddCheckout("master", "origin/master");
                            //bat.AddCheckout("release", "origin/release");

                            bat.AddCD(startingFolder);

                            bat.Write();
                            bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));

                            return Json(new JsonMessage(true, "Success"));
                        }
                    }
                    else
                        return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}" ));
                }
                else
                    return Json(new JsonMessage(false, $"Unable to find starting folder {startingFolder}" ));

            }
            catch (System.Exception ex)
            {
                return Json(new JsonMessage(false, $"{repoName} - Failed to clone - [{ex.Message}]" ));
            }
        }
        public JsonResult PullRepository(string guid)
        {
            // see if starting folder from login exists

            var startingFolder = string.Empty;
            var repoName = string.Empty;

            try
            {
                CreateStartingFolder(ref startingFolder);

                // See or create sub-folder based on PROJECT name

                if (System.IO.Directory.Exists(startingFolder))
                {
                    CreateStartingProjectFolder(ref startingFolder);

                    var mainBat = startingFolder + @"\PullRepository.bat";

                    if (System.IO.File.Exists(mainBat))
                        System.IO.File.Delete(mainBat);

                    var bat = new Classes.GitBatch(mainBat)
                    {
                        Redirect = true,
                        RedirectError = $@"{startingFolder.Replace(@"\\", @"\")}\ErrorPull.txt",
                        RedirectFile = $@"{startingFolder.Replace(@"\\", @"\")}\OutputPull.txt"
                    };

                    var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                    var repo = vsts.GetRepositoryOnGuid(guid);

                    repoName = repo.Name;

                    // Now clear the repo folder if there
                    if (System.IO.Directory.Exists(startingFolder))
                        ClearFolder(startingFolder + $"\\{repoName}");

                    if (System.IO.Directory.Exists(startingFolder + $"\\{repoName}"))
                        System.IO.Directory.Delete(startingFolder + $"\\{repoName}");

                    int ndx = repo.RemoteUrl.LastIndexOf(@"/");
                    string theFile = repo.RemoteUrl.Substring(ndx + 1);

                    bat.AddLine($"echo start");
                    bat.AddLine($"echo Working on '{theFile}'");
                    if (!System.IO.Directory.Exists($@"{startingFolder}\{theFile}"))
                    {
                        bat.AddCD(startingFolder);
                        bat.AddLine($@"git clone {repo.RemoteUrl}");
                    }
                    else
                    {
                        bat.AddCD(startingFolder);
                        bat.AddLine($@"git clone --bare {repo.RemoteUrl}");
                        bat.AddLine("git config core.bare false");
                    }
                    bat.AddCD($@"{startingFolder}\{theFile}");
                    bat.AddCheckout("master", "origin/master");
                    bat.AddCheckout("release", "origin/release");
                    //bat.AddLine($"git checkout -b master origin/master");

                    bat.AddCD(startingFolder);

                    bat.Write();
                    bat.GitRun(startingFolder, HttpContext.Session.GetString(Session.USER), HttpContext.Session.GetString(Session.GIT));
                }

                return Json(new JsonMessage(true, repoName + " : Pulled" ));
            }
            catch (System.Exception ex)
            {
                return Json(new JsonMessage(false, $"{repoName} - Failed to clone - [{ex.Message}]" ));
            }
        }
        public JsonResult RemoveRepository(string guid)
        {
            var startingFolder = string.Empty;
            var repoName = string.Empty;

            try
            {
                var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                var repo = vsts.GetRepositoryOnGuid(guid);

                repoName = repo.Name;

                CreateStartingFolder(ref startingFolder);

                // See or create sub-folder based on PROJECT name

                if (System.IO.Directory.Exists(startingFolder))
                {
                    CreateStartingProjectFolder(ref startingFolder);

                    // Now clear the repo folder if there
                    if (System.IO.Directory.Exists(startingFolder))
                        ClearFolder(startingFolder + $"\\{repoName}");

                    if (System.IO.Directory.Exists(startingFolder + $"\\{repoName}"))
                        System.IO.Directory.Delete(startingFolder + $"\\{repoName}");

                    if (System.IO.Directory.Exists(startingFolder + $"\\{repoName}"))
                        return Json(new JsonMessage(false, $"Failed to delete [{repoName}]"));
                    else
                        return Json(new JsonMessage(true, repoName + " folder and code : REMOVED"));

                }
                else
                    return Json(new JsonMessage(false, $"Failed to delete [{repoName}]"));
            }
            catch (System.Exception ex)
            {
                return Json(new JsonMessage(false, $"Failed to delete [{repoName}]- [{ex.Message}]"));
            }
        }
        public JsonResult BuildBuildListStatus()
        {
            try
            {
                Classes.VSTS.VstsBuildDefinitions defs;

                if (_inBuildProcess)
                {
                    while (!_cache.TryGetValue(CACHE.BUILDS, out defs)) { }
                    return Json(new JsonMessage(true, "Done"));
                }
                else
                {
                    _cache.TryGetValue(CACHE.BUILDS, out defs);
                    return Json(new JsonMessage(defs != null, "Done"));
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new JsonMessage(false, "Done"));
            }
            
        }

        public JsonResult BuildReleaseListStatus()
        {
            Classes.VSTS.VstsReleaseDefinitions defs;

            if (_inReleaseProcess)
            {
                while (!_cache.TryGetValue(CACHE.RELEASES, out defs)) { }
                return Json(new JsonMessage(true, "Done"));
            }
            else
            {
                _cache.TryGetValue(CACHE.RELEASES, out defs);

                return Json(new JsonMessage(defs != null, "Done"));
            }            
        }

        public void CreateBMR(string guid)
        {
            var x = guid;   // 446 is passed back
        }

        public JsonResult GetWorkItemLink(string id)
        {
            var item = GetWorkItem(id);

            if (item != null)
                return Json(new { link = item.Links.Html.HRef });
            else
                return Json(new { link = "PBI Not Found" });

            // return Json(new { link = vsts.WebReleasesForId(HttpContext.Session.GetString(Session.SELECTED_PROJ), id) });
        }

    }
}


///TODO:
///  Replace FEATURE with an application setting value
///  Replace RELEASE main name
///  Replace MASTER main name
///                menu.MenuItems.Add(new MenuItem("Report on Completed", (o, args) => FeatureBranchReportOnCompleted()));
///                menu.MenuItems.Add(new MenuItem("Report on All Features", (o, args) => FeatureBranchReportOnFeatures()));
///                menu.MenuItems.Add(new MenuItem("Create Initial Branches", (o, args) => CreateInitialBranches(Repositories.SelectedItems[0])));
///                inventory
///                search tokens/variables
///                search packages
///                search work items???
///                search solutions
///                scan/search source - include text and file extensions to search by
///                show GIT branching
///                Saving login values somewhere
///                getting appsettings for VSTS (MrCooper)