using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Kendo.Mvc.Extensions;
using System.Linq;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using AstuteCommander.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

using AstuteCommander.Classes.Azure.AzureCosmosDB;

namespace AstuteCommander.Controllers
{
    public class HomeController : AstuteController
    {
        public HomeController(ILogger<RepositoryController> logger,
                                    IMemoryCache memCache,
                                    IAppSettings settings,
                                    IHostingEnvironment env) : base(memCache, settings, env)
        {
            Classes.Raz.root = env.WebRootPath;
            //_logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult KeepSessionAlive()
        {
            HttpContext.Session.SetString(Session.HEARTBEAT, System.DateTime.Now.ToString());

            return Json(new JsonMessage(false, "No Message"));
        }


        public IActionResult Error()
        {
            return View();
        }



    }
}
