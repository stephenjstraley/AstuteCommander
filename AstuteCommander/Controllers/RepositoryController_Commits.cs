using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;
using System.Linq;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public ActionResult RepoCommits(string id)
        {
            ViewBag.GUID = id;

            return PartialView();
        }

        public ActionResult GetRepoCommits([DataSourceRequest]DataSourceRequest request, string id)
        {
            var r = GetRepositoryCommits(id);

            if (r == null)
            {
//                var asdasd = "Stop here";
            }

            List<RepoCommitVM> commits = new List<RepoCommitVM>();

            foreach (var item in r.Commits)
            {
                commits.Add(new RepoCommitVM()
                {
                    Author = item.Author.Name,
                    AuthorDate = System.Convert.ToDateTime(item.Author.Date).ToString(),
                    Comment = item.Comment,
                    Committer = item.Committer.Name,
                    CommitterDate = System.Convert.ToDateTime(item.Committer.Date).ToShortDateString(),
                    TheDate = System.Convert.ToDateTime(item.Author.Date),
                    Guid = item.Guid,
                });
            }

            var results = commits.OrderByDescending(t => t.TheDate).ToList();

            return Json(results.ToDataSourceResult(request));
        }
    }
}
