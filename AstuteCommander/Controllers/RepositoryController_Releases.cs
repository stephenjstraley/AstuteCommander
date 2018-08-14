using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using AstuteCommander.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Data;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public ActionResult RepoReleases(string id)
        {
            ViewBag.GUID = id;

            var builds = GetBuildDefinition(id);
            List<Classes.VSTS.VstsReleaseDefinition> items = new List<Classes.VSTS.VstsReleaseDefinition>();

            // Now that I have the builds definitions for the selected REPO, I need to search each
            // found build to associate the a "found" release

            //Parallel.ForEach(builds, new ParallelOptions { MaxDegreeOfParallelism = 1 }, build =>
            foreach(var build in builds)
            {
                // Find the releases with the same BUILD.ID
                var defs = GetReleaseDefinitionOnBuildId(build.Id);
                if (defs.Count > 0)
                {
                    items.AddRange(defs);
                }
            }//);

            var tabs = new List<BuildTabVM>();

            // Now one at a time go through and pull out NAME and ID for the model
            //Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 1 }, item =>
            foreach(var item in items)
            {
                var b = new BuildTabVM()
                {
                    Comment = item.Description,
                    Id = item.Id,
                    Name = item.Name
                };
                tabs.Add(b);
            }//);

            return PartialView(tabs);
        }

        public ActionResult RepoReleaseDetails([DataSourceRequest]DataSourceRequest request, string id)
        {
            ViewBag.ReleaseId = id;

            return PartialView();
        }

        public ActionResult GetReleaseDefinition([DataSourceRequest]DataSourceRequest request, string id)
        {
            Classes.VSTS.VstsReleaseDefinition def = null;

            while (_cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES) == null) {}

            def = _cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES).ReleaseDefinitions.Where(t => t.Id == id).FirstOrDefault();

            List<RepoReleaseDefinitionVM> details = new List<RepoReleaseDefinitionVM>();

            if (def != null)
            {
                details.Add(new RepoReleaseDefinitionVM()
                {
                    Id = def.Id,
                    CreatedOn = System.Convert.ToDateTime(def.CreatedOn),
                    ModifiedOn = System.Convert.ToDateTime(def.ModifiedOn),
                    Revisions = System.Convert.ToInt32(def.Revision)
                });
            }

            return Json(details.ToDataSourceResult(request));
        }
        public ActionResult RepoReleaseReleases([DataSourceRequest]DataSourceRequest request, string releaseid)
        {
            ViewBag.ReleaseId = releaseid;

            return PartialView();
        }
        public ActionResult GetReleasesForDefinition([DataSourceRequest]DataSourceRequest request, string defId)
        {
            Classes.VSTS.VstsReleases items = GetReleases(defId);

            List<RepoReleaseVM> details = new List<RepoReleaseVM>();

            foreach (var item in items.Release)
            {
                details.Add(new RepoReleaseVM()
                {
                    CreatedOn = System.Convert.ToDateTime(item.CreatedOn),
                    Description = item.Description,
                    Id = item.Id,
                    ModifiedOn = System.Convert.ToDateTime(item.ModifiedOn),
                    Name = item.Name,
                    Reason = item.Reason,
                    Status = item.Status,
                    VstsLink = item.Links.Web.HRef
                });
            }

            return Json(details.ToDataSourceResult(request));
        }

        public ActionResult RepoReleaseEnvironments([DataSourceRequest]DataSourceRequest request, string releaseid)
        {
            ViewBag.ReleaseId = releaseid;

            return PartialView();
        }

        public ActionResult GetEnvironmentsForDefinition([DataSourceRequest]DataSourceRequest request, string defId)
        {
            Classes.VSTS.VstsReleaseDefinition def = null;

            while (_cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES) == null) { }

            def = _cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES).ReleaseDefinitions.Where(t => t.Id == defId).FirstOrDefault();

            List<RepoReleaseEnvironmentVM> details = new List<RepoReleaseEnvironmentVM>();

            if (def != null)
            {
                foreach (var env in def.Environments)
                {
                    var newEnv = new RepoReleaseEnvironmentVM()
                    {
                        DefId = def.Id,
                        Id = env.Id,
                        DaysToKeep = env.RetentionPolicy.DaysToKeep,
                        Name = env.Name,
                        Rank = env.Rank,
                        ReleasesToKeep = env.RetentionPolicy.ReleasesToKeep
                    };
                    foreach (var tsk in env.DeployPhases)
                    {
                        foreach (var phs in tsk.WorkflowTasks)
                        {
                            if (!string.IsNullOrEmpty(phs.Inputs.ApplicationDirectory))
                                newEnv.ApplicationDirectory = phs.Inputs.ApplicationDirectory;
                            if (!string.IsNullOrEmpty(phs.Inputs.AppPoolName))
                                newEnv.AppPoolName = phs.Inputs.AppPoolName;
                            if (!string.IsNullOrEmpty(phs.Inputs.MachinesList))
                                newEnv.MachinesList = phs.Inputs.MachinesList;
                        }
                        
                    }


                    details.Add(newEnv);
                }
            }

            return Json(details.ToDataSourceResult(request));
        }

        public ActionResult RepoReleaseTokens([DataSourceRequest]DataSourceRequest request, string releaseid)
        {
            ViewBag.ReleaseId = releaseid;

            var item = BuildTokenData(releaseid);

            return PartialView(item);
        }

        public ActionResult GetVariablesForDefinition([DataSourceRequest]DataSourceRequest request, string releaseid)
        {
            RepoReleaseVariableVM item = BuildTokenData(releaseid);

            return Json(item.Data.ToDataSourceResult(request));
        }

        public RepoReleaseVariableVM BuildTokenData(string releaseId)
        {
            Classes.VSTS.VstsReleaseDefinition def = null;

            ////TODO: add timeout stuff
            while (_cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES) == null) { }

            def = _cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES).ReleaseDefinitions.Where(t => t.Id == releaseId).FirstOrDefault();

            RepoReleaseVariableVM item = new RepoReleaseVariableVM();
            item.Columns = new List<VSTSDataColumn>();
            List<List<string>> tempData = new List<List<string>>();
            if (def != null)
            {
                // add columns
                var tok = new VSTSDataColumn("Token", typeof(string));
                tok.Caption = "Token";
                tok.IsLocked = true;
                item.Columns.Add(tok);
                foreach (var col in def.Environments)
                {
                    tok = new VSTSDataColumn(col.Name, typeof(string));
                    tok.Caption = col.Name;
                    tok.IsLocked = false;

                    tok.ColumnName = System.Text.RegularExpressions.Regex
                                      .Replace(tok.ColumnName, @"[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;><!@#&\-\+\ /]", string.Empty);

                    item.Columns.Add(tok);
                }
                // now values.
                foreach (var env in def.Environments)
                {
                    foreach (var v in env.Variables)
                    {
                        string varName = v.Key;
                        string varValue = ((Classes.VSTS.VstsValue)v.Value).Value;
                        string envName = env.Name;

                        // I could use RANK but to be safe, I'll use the column position to know where to put
                        // variable value
                        bool found = false;
                        int foundPos = -1;
                        for (int j = 0; j < tempData.Count; j++)
                        {
                            if (tempData[j][0] == varName)
                            {
                                found = true;
                                foundPos = j;
                                break;
                            }
                        }
                        var pos = item.Columns.FindIndex(t => t.Caption == envName);

                        if (!found)
                        {
                            var toAdd = new List<string>();
                            for (int x = 0; x < item.Columns.Count; x++)
                                toAdd.Add(string.Empty);
                            toAdd[0] = varName;
                            toAdd[pos] = varValue;
                            tempData.Add(toAdd);
                        }
                        else // found so obtain position of env
                        {
                            var fullItem = tempData[foundPos];
                            fullItem[pos] = varValue;
                        }
                    }
                }

                // Now build dictionary
                item.Data = new List<Dictionary<string, string>>();
                foreach (var element in tempData)
                {
                    var itemToAdd = new Dictionary<string, string>();
                    for (int x = 0; x < item.Columns.Count; x++)
                    {
                        itemToAdd.Add(item.Columns[x].ColumnName, element[x]);
                    }
                    item.Data.Add(itemToAdd);
                }
            }
            return item;
        }

        public ActionResult RepoReleaseWorkflows([DataSourceRequest]DataSourceRequest request, string id)
        {
            return PartialView();
        }

    }
}
