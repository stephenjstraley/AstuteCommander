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
using Newtonsoft.Json;

using AstuteCommander.Classes.Azure.AzureCosmosDB;

namespace AstuteCommander.Controllers
{
    public class CustomersController : AstuteController
    {
        public CustomersController(ILogger<RepositoryController> logger,
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

        public IActionResult GetCustomers([DataSourceRequest]DataSourceRequest request)
        {
            // Convert Hub string to object to get ID and things
            //Astute.Data.Models.Old.Entities.companies companies = JsonConvert.DeserializeObject<Astute.Data.Models.AstuteHub>(hubString);

            // Connection to data services
            var repo = new Astute.Data.Services.AstuteDataRepo(new Astute.Data.Services.AstuteDataContext());

            //var startingFolder = string.Empty;
            var companies = repo.GetCompanies();

            try
            {
                //CreateStartingFolder(ref startingFolder);

                //var vsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));

                

                //var repos = vsts.GetRepos(projectName);
                //var items = repos.Repositories.OrderBy(t => t.Name).ToList();
                //Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 1 }, item =>
                //{
                //    var toAdd = new RepositoryVM()
                //    {
                //        Guid = item.Guid,
                //        Name = item.Name,
                //        Url = item.RemoteUrl,
                //        DefaultBranch = item.DefaultBranch,
                //        DirectoryOnDisk = false
                //    };
                //    if (System.IO.Directory.Exists(startingFolder))
                //    {
                //        var projectStartingFolder = startingFolder;
                //        CreateStartingProjectFolder(ref projectStartingFolder);
                //        if (System.IO.Directory.Exists(startingFolder))
                //        {
                //            toAdd.DirectoryOnDisk = System.IO.Directory.Exists(projectStartingFolder + @"\" + item.Name);
                //        }
                //    }
                //    results.Add(toAdd);
                //});
            }
            catch (System.Exception foo)
            {
                System.Diagnostics.Debug.WriteLine($"Get Repos: {foo.Message}");
            }
            return Json(companies.ToDataSourceResult(request));
        }


    }
}
