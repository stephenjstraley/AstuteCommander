using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;

namespace AstuteCommander.Controllers
{
    public partial class RepositoryController
    {
        public ActionResult RepoPushes(string id)
        {
            ViewBag.GUID = id;

            return PartialView();
        }

        public ActionResult GetRepoPushes([DataSourceRequest]DataSourceRequest request, string id)
        {
            var pr = GetRepositoryPushes(id);

            List<RepoPushVM> pushes = new List<RepoPushVM>();

            foreach(var item in pr.Pushes)
            {
                var toAdd = new RepoPushVM();
                toAdd.PushedBy = item.PushedBy.By;
                toAdd.Date = System.Convert.ToDateTime(item.Date);
                toAdd.Id = item.Id;

                pushes.Add(toAdd);
            }

            return Json(pushes.ToDataSourceResult(request));
        }
    }
}
