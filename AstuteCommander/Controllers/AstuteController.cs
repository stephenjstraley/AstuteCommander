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
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AstuteCommander.Controllers
{
    public class AstuteController : Controller
    {
        protected IMemoryCache _cache;
        protected IAppSettings _settings;
        protected IHostingEnvironment _env;

        protected static int MAXTIMES_TRY = 100000;

        protected AuthenticationResult _result;

        public AstuteController(IMemoryCache memCache, IAppSettings settings, IHostingEnvironment env) : base()
        {
            _cache = memCache;
            _settings = settings;
            _env = env;
        }

        public string AuthToken
        {
            get
            {
                if (_result == null)
                {
                    GetToken();
                    return _result.AccessToken;
                }
                else
                {
                    if (System.DateTime.UtcNow < _result.ExpiresOn.UtcDateTime)
                        GetToken();

                    return _result.AccessToken;
                }
            }
        }




        private void GetToken()
        {
            var httpClient = new HttpClient();
            var authContext = new AuthenticationContext(_settings.Authority);

            var clientCredential = new ClientCredential(_settings.ClientId, _settings.AppKey);

            _result = authContext.AcquireTokenAsync(_settings.Resource, clientCredential).Result;
        }
















        protected void CreateStartingFolder(ref string folder)
        {
            ///TODO ... VSTS sometimes returns NULL
            folder = HttpContext.Session.GetString(Session.VSTS);

            if (folder == null)
            // reset connection?
            { }
            else
            {
                // see or create sub-folder based on PROJECT name
                if (!folder.EndsWith(@"\"))
                    folder += @"\";

                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);
            }
        }
        protected void CreateStartingProjectFolder(ref string folder)
        {
            folder += HttpContext.Session.GetString(Session.SELECTED_PROJ);

            folder = folder.Replace("visual sudio 2017", "\"visual sudio 2017\"");

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);
        }
        protected void ClearFolder(string FolderName)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FolderName);

            if (dir.Exists)
            {
                foreach (System.IO.FileInfo fi in dir.GetFiles())
                {
                    fi.IsReadOnly = false;
                    fi.Delete();
                }

                foreach (System.IO.DirectoryInfo di in dir.GetDirectories())
                {
                    ClearFolder(di.FullName);
                    di.Delete();
                }
            }
        }


        public Classes.VSTS.VstsBranches GetRepositoryBranches(string guid)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryBranches(guid);
        }
        public Classes.VSTS.VstsBranches GetRepositoryBranchesAndStats(string guid)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryBranchesAndStats(guid);
        }
        public Classes.VSTS.VstsBranches GetRepositoryFeatureBranches(string guid)
        {
            var fbprefix = _settings.Setting.GetSection("FeatureBranchPrefix").Value ?? "feature/";
//            if (fbprefix.EndsWith("/"))
//                fbprefix = fbprefix.Substring(0, fbprefix.Length - 1);

            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryFeatureBranches(guid, fbprefix);
        }
        public Classes.VSTS.VstsCommits GetRepositoryCommits(string guid)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryCommits(guid);
        }
        public Classes.VSTS.VstsPullRequests GetRepositoryPullRequests(string guid)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryPullRequests(guid);
        }
        public Classes.VSTS.VstsWorkItems GetRepositoryPullRequestWorkItems(string guid, string id)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryPullRequestWorkItem(guid, id);
        }
        public Classes.VSTS.VstsWorkItem GetRepositoryWorkItem(string guid, string id)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetWorkItem(HttpContext.Session.GetString(Session.SELECTED_PROJ), id);
        }
        public Classes.VSTS.VstsPushes GetRepositoryPushes(string guid)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetRepositoryPushes(guid);
        }
        public Classes.VSTS.VstsBuilds GetRepositoryBuilds(string buildId)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetBuilds(HttpContext.Session.GetString(Session.SELECTED_PROJ), buildId);
        }
        public Classes.VSTS.VstsReleases GetReleases(string releaseId)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetReleases(HttpContext.Session.GetString(Session.SELECTED_PROJ), releaseId);
        }
        public Classes.VSTS.VstsWorkItem GetWorkItem(string id)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return vsts.GetWorkItem(HttpContext.Session.GetString(Session.SELECTED_PROJ), id);
        }
        public JsonResult GetBuildsLinkForId(string id)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return Json(new { link = vsts.WebBuildsForId(HttpContext.Session.GetString(Session.SELECTED_PROJ), id) });
        }
        public JsonResult GetReleasesLinkForId(string id)
        {
            var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
            return Json(new { link = vsts.WebReleasesForId(HttpContext.Session.GetString(Session.SELECTED_PROJ), id) });
        }
        public List<Classes.VSTS.VstsBuildDefinition> GetBuildDefinition(string repoGuid)
        {
            Classes.VSTS.VstsBuildDefinitions defs = null;

            var timesThrough = 0;

            while (defs == null && timesThrough++ < MAXTIMES_TRY)
            {
                defs = _cache.Get<Classes.VSTS.VstsBuildDefinitions>(CACHE.BUILDS);
            }

            if (defs != null)
            {
                if (!((defs.BuildDefinitions?.Count ?? 0) > 0))
                {
                    defs = new Classes.VSTS.VstsBuildDefinitions()
                    {
                        BuildDefinitions = new List<Classes.VSTS.VstsBuildDefinition>()
                    };
                }
            }

            var items = new List<Classes.VSTS.VstsBuildDefinition>();
            try
            {
                foreach (var t in defs.BuildDefinitions)
                {
                    var ttt = t.Repository;

                    if (ttt.Id == repoGuid)
                        items.Add(t);
//                    items = defs.BuildDefinitions.Where(t => t.Repository.Id == repoGuid).ToList();
                }
            }
            catch (System.Exception ee)
            {
                System.Diagnostics.Debug.WriteLine($"Get Build Definitions -----> {ee.Message}");
            }
            return items;
        }
        public List<Classes.VSTS.VstsReleaseDefinition> GetReleaseDefinitionOnBuildId(string id)
        {
            var items = new List<Classes.VSTS.VstsReleaseDefinition>();
            Classes.VSTS.VstsReleaseDefinitions defs = null;

            var timesThrough = 0;

            while (defs == null && timesThrough++ < MAXTIMES_TRY)
            {
                defs = _cache.Get<Classes.VSTS.VstsReleaseDefinitions>(CACHE.RELEASES);
            }
            if (defs != null)
            {
                if (!((defs.ReleaseDefinitions?.Count ?? 0) > 0))
                {
                    defs = new Classes.VSTS.VstsReleaseDefinitions()
                    {
                        ReleaseDefinitions = new List<Classes.VSTS.VstsReleaseDefinition>()
                    };
                }

                var theCount = 1;
                System.Diagnostics.Debug.WriteLine($"Release definitions are {defs.ReleaseDefinitions.Count}");

                foreach (var def in defs.ReleaseDefinitions)
                {
                    System.Diagnostics.Debug.WriteLine($"On {theCount++}");

                    if (def.Artifacts != null)
                    {
                        foreach (var art in def.Artifacts)
                        {
                            var ttt = art.DefinitionReference;

                            var tttt = ttt.Definition;

                            if (tttt.Id == id)  // This release is connected to the build
                               items.Add(def);
                        }
                    }
                }
            }

            return items;
        }


    }
}
