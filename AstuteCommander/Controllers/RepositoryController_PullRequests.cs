using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public ActionResult RepoPullRequests(string id)
        {
            ViewBag.GUID = id;

            return PartialView();
        }

        public ActionResult GetRepoPullRequests([DataSourceRequest]DataSourceRequest request, string id)
        {

            var pr = GetRepositoryPullRequests(id);

            List<RepoPullRequestVM> prs = new List<RepoPullRequestVM>();

            foreach(var item in pr.PullRequests)
            {
                var workItems = GetRepositoryPullRequestWorkItems(id, item.PRId);

                var toAdd = new RepoPullRequestVM();
                toAdd.CreatedBy = item.CreatedBy.By;
                toAdd.Date = System.Convert.ToDateTime(item.CreationDate);
                toAdd.MergeStatus = item.MergeStatus;
                toAdd.Number = item.PRId;
                toAdd.SourceBranch = item.SourceBranch;
                toAdd.Status = item.Status;
                toAdd.TargetBranch = item.TargetBranch;
                toAdd.Title = item.Title;

                if (workItems != null)
                    toAdd.WorkItems = workItems.Count;

                prs.Add(toAdd);
            }

            return Json(prs.ToDataSourceResult(request));
        }
    }
}
