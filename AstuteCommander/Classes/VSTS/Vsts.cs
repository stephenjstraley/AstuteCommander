using System.Collections.Generic;
using System.Linq;
using AstuteCommander.Classes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace AstuteCommander.Classes.VSTS
{
    public class Vsts
    {
        static string ORG = string.Empty;  // "mrcooper";
        static string TOKEN = string.Empty;
        static string ORG_V = ".vsrm";

        ILogger<Vsts> _logger;

        public static void SetOrg(string value)
        {
            ORG = value;
        }
        public Vsts(string token = "")
        {
            TOKEN = token;
        }
        public Vsts(ILogger<Vsts> logger)
        {
            _logger = logger;
        }
        public string WebBuildsForId(string project, string id)
        {
            return $"https://{ORG}.visualstudio.com/{project}/_build/index?context=allDefinitions&path=%5Ctwo-phase-builds&definitionId={id}";
        }
        public string WebReleasesForId(string project, string id)
        {
            return $"https://{ORG}.visualstudio.com/{project}/_release?definitionId={id}";
        }

        public VstsRepositories GetRepos(string project)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"{project}/_apis/git/repositories/?api-version=2.0";

            return new VstsRest(config).GetItem<VstsRepositories>();
        }
        public VstsProjects GetProjects()
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += "_apis/projects?stateFilter=All&api-version=2.0";

            return new VstsRest(config).GetItem<VstsProjects>();
        }
        public VstsRepository GetRepository(string url)
        {
            var config = new Configuration(ORG, TOKEN)
            {
                UriString = url
            };

            return new VstsRest(config).GetItem<VstsRepository>();
        }
        public VstsRepository GetRepositoryOnGuid(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}";

            return new VstsRest(config).GetItem<VstsRepository>();
        }


        public VstsBranches GetRepositoryBranches(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/refs";

            return new VstsRest(config).GetItem<VstsBranches>();
        }
        public VstsBranches GetRepositoryBranchesAndStats(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/refs?api-version=2.0&includeStatuses=true";

            return new VstsRest(config).GetItem<VstsBranches>();
        }
        public VstsBranches GetRepositoryFeatureBranches(string guid, string fp)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/refs/heads/{fp}";

            return new VstsRest(config).GetItem<VstsBranches>();
        }

        public VstsCommits GetRepositoryCommits(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/commits?api-version=2.0";

            return new VstsRest(config).GetItem<VstsCommits>();
        }
        public VstsPullRequests GetRepositoryPullRequests(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/pullRequests?api-version=2.0";

            return new VstsRest(config).GetItem<VstsPullRequests>();
        }

        public VstsPullRequest GetRepositoryPullRequest(string guid, string id)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/pullRequests/{id}";

            return new VstsRest(config).GetItem<VstsPullRequest>();
        }

        public VstsWorkItems GetRepositoryPullRequestWorkItem(string guid, string id)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/pullRequests/{id}/workitems";

            return new VstsRest(config).GetItem<VstsWorkItems>();
        }

        public VstsWorkItem GetWorkItem(string project, string id)  // Project is not used - DefaultCollection is used instead
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"DefaultCollection/_apis/wit/workItems/{id}";

            return new VstsRest(config).GetItem<VstsWorkItem>();
        }

        public VstsPushes GetRepositoryPushes(string guid)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"_apis/git/repositories/{guid}/pushes?api-version=2.0";

            return new VstsRest(config).GetItem<VstsPushes>();
        }

        public VstsBuildDefinitions GetBuildDefinitions(string project)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"{project}/_apis/build/definitions?api-version=2.0";

            return new VstsRest(config).GetItem<VstsBuildDefinitions>();
        }
        public VstsReleaseDefinitions GetReleaseDefitions(string project)
        {
            var config = new Configuration(ORG + ORG_V, TOKEN);
            config.UriString += $"{project}/_apis/Release/definitions?api-version=3.0-preview.1";

            return new VstsRest(config).GetItem<VstsReleaseDefinitions>();
        }
        public VstsReleases GetReleases(string project, string releaseId)
        {
            var config = new Configuration(ORG + ORG_V, TOKEN);
            config.UriString += $"{project}/_apis/Release/releases?definitionId={releaseId}";

            return new VstsRest(config).GetItem<VstsReleases>();
        }

        public VstsBuildDefinitions GetDetiledBuildDefinitions(string project)
        {
            VstsBuildDefinitions items;

            try
            {
                items = GetBuildDefinitions(project);

                // Now one at a time, go through each and fill out the remaining information
                var config = new Configuration(ORG, TOKEN);

                for (int i = 0; i < items.BuildDefinitions.Count; i++)
                //                Parallel.For(0, items.BuildDefinitions.Count, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i =>
                {
                    var temp = items.BuildDefinitions[i];
                    //config.UriString = items.BuildDefinitions[i].Url;
                    config.UriString = $"https://mrcooper.visualstudio.com/Originations/_apis/build/definitions/{temp.Id}?api-version=2.0";
                    var rest = new VstsRest(config);

                    //System.Threading.Thread.Sleep(500);
                    System.Diagnostics.Debug.WriteLine($"--Builds -- [{i}]-[{items.BuildDefinitions.Count}]--- {config.UriString} ------------------------------");

                    items.BuildDefinitions[i] = rest.GetItem<VstsBuildDefinition>();
                }
                System.Diagnostics.Debug.WriteLine("DONE WITH BUILDS ------------------------------");
                //                );
            }
            catch (System.Exception ee)
            {
                var asd = ee.Message;
                items = new VstsBuildDefinitions()
                {
                    BuildDefinitions = new List<VstsBuildDefinition>()
                };
                System.Diagnostics.Debug.WriteLine($"EMPTY builds ----[{ee.Message}]--------------------------");
            }


            return items;
        }

        public VstsReleaseDefinitions GetDetailedReleaseDefinitions(string project)
        {
            VstsReleaseDefinitions items;

            try
            {
                items = GetReleaseDefitions(project);
                var config = new Configuration(ORG_V, TOKEN);

                for (int i = 0; i < items.ReleaseDefinitions.Count; i++)
 //                Parallel.For(0, items.ReleaseDefinitions.Count, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i =>
                {
                    config.UriString = items.ReleaseDefinitions[i].Url;
                    var rest = new VstsRest(config);
                    items.ReleaseDefinitions[i] = rest.GetItem<VstsReleaseDefinition>();

                    System.Diagnostics.Debug.WriteLine($"--Release -- {config.UriString} ------------------------------");
                }
//                );
                System.Diagnostics.Debug.WriteLine("DONE WITH RELEASES ------------------------------");

            }
            catch (System.Exception ex)
            {
                var asd = ex.Message;
                items = new VstsReleaseDefinitions()
                {
                    ReleaseDefinitions = new List<VstsReleaseDefinition>()
                };
                System.Diagnostics.Debug.WriteLine($"EMPTY RELEASES ----[{ex.Message}]--------------------------");
            }

            return items;
        }

        public VstsBuilds GetBuilds(string project, string id)
        {
            var config = new Configuration(ORG, TOKEN);
            config.UriString += $"{project}/_apis/build/builds?definitions={id}&api-version=2.0";

            return new VstsRest(config).GetItem<VstsBuilds>();
        }

    }
}
