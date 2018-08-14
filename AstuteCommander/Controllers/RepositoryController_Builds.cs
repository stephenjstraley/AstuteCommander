using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using AstuteCommander.Models;
using System.Collections.Generic;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public ActionResult RepoBuilds(string id)
        {
            ViewBag.GUID = id;

            // Now build the tab model for the partial view
            // Grab the build definitions for the selected REPO ID/GUID

            var builds = GetBuildDefinition(id);

            var items = new List<BuildTabVM>();

            // Now one at a time go through and pull out NAME and ID for the model
            Parallel.ForEach(builds, new ParallelOptions { MaxDegreeOfParallelism = 1 }, build =>
            {
                var b = new BuildTabVM()
                {
                    Comment = build.Comment,
                    Id = build.Id,
                    Name = build.Name,
                    RepositoryGuid = build.Repository.Id
                };
                items.Add(b);
            });

            return PartialView(items);
        }

        public ActionResult RepoBuildDetails([DataSourceRequest]DataSourceRequest request, string id)
        {
            ViewBag.BuildId = id;

            return PartialView();
        }

        public ActionResult GetRepoBuildDetails([DataSourceRequest]DataSourceRequest request, string id)
        {
            var bs = GetRepositoryBuilds(id);

            List<RepoBuildDetailVM> details = new List<RepoBuildDetailVM>();

            foreach (var item in bs.Builds)
            {
                var d = new RepoBuildDetailVM()
                {
                    Build = item.BuildNumber,
                    Id = item.Id,
                    Result = item.Result,
                    Status = item.Status,
                    StartTime = System.Convert.ToDateTime(item.StartTime),
                    EndTime = System.Convert.ToDateTime(item.FinishTime),
                    VstsLink = item.Links.Web.HRef
                };
                details.Add(d);
            }

            return Json(details.ToDataSourceResult(request));
        }
    }
}
