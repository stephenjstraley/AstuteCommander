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
        public IActionResult ClientRepository()
        {
            return View();
        }

        public IActionResult GetCosmosDbAccounts([DataSourceRequest]DataSourceRequest request)
        {
            var token = AuthToken;

            AstuteCommander.Classes.Azure.AzureCosmosDB.Model.CosmosDbAccountList list = AstuteCommander.Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetAccountList(token, _settings.SubscriptionId);

            List<CosmosAccountsVM> accounts = new List<CosmosAccountsVM>();
            foreach (var item in list.Items)
            {
                accounts.Add(new CosmosAccountsVM()
                {
                    AccountId = item.Id,
                    Endpoint = item.Properties.DocumentEndpoint,
                    Name = item.Name,
                    PrimaryMasterKey = item.Keys.PrimaryMasterKey,
                    SecondaryMasterKey = item.Keys.SecondaryMasterKey,
                    Group = item.ResourceGroup
                });
            }

            //            Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetCollectionList(token, "ahcdoc-ast", "ahc");

            //Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetDbList(token, accounts[0].Name);

//          var things = Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetDbList(accounts[2].Endpoint, accounts[2].PrimaryMasterKey);
           
            

            return Json(accounts.ToDataSourceResult(request));
        }

        public IActionResult GetCosmosDbAccountsDbs([DataSourceRequest]DataSourceRequest request, string accountName, string ep, string key)
        {
            var dbs = Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetDbList(ep, key);
            List<CosmosDbAccountDbVM> databases = new List<CosmosDbAccountDbVM>();
            foreach(var db in dbs.Items)
            {
                databases.Add(new CosmosDbAccountDbVM()
                {
                    DbAccountName = accountName,
                    DbEndpoint = ep,
                    DbName = db.Id,
                    DbPrimaryMasterKey = key
                });
            }

            return Json(databases.ToDataSourceResult(request));
        }

        public IActionResult GetCosmosDbAccountsDbCols([DataSourceRequest]DataSourceRequest request, string accountName, string ep, string key, string db)
        {
            var colls = Classes.Azure.AzureCosmosDB.AzureCosmosDB.GetDbCollectionList(ep, key, db);
            List<CosmosDbAccountDbCollectionVM> collections = new List<CosmosDbAccountDbCollectionVM>();
            foreach(var col in colls.Items)
            {
                collections.Add(new CosmosDbAccountDbCollectionVM()
                {
                    ColName = col.Id,
                    DbColAccountName = accountName,
                    DbColEndpoint = ep,
                    DbColPrimaryMasterKey = key,
                    DbColName = db
                });
            }

            return Json(collections.ToDataSourceResult(request));
        }

        public IActionResult GetCollection([DataSourceRequest]DataSourceRequest request, string ep, string key, string db, string collection)
        {
            using (var client = new DocumentClient(new System.Uri(ep), key))
            {
                IDocumentQuery<CollectionVM> query = client.CreateDocumentQuery<CollectionVM>(
                UriFactory.CreateDocumentCollectionUri(db, collection),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

                List<CollectionVM> results = new List<CollectionVM>();
                while (query.HasMoreResults)
                {
                    results.AddRange(query.ExecuteNextAsync<CollectionVM>().Result);
                }

                results.ForEach(t => 
                              {
                                  t.ActualEndpoint = ep;
                                  t.ActualColName = collection;
                                  t.ActualDbName = db;
                                  t.ActualPrimaryMasterKey = key;
                               });

                return Json(results.ToDataSourceResult(request));
            }
        }

        public IActionResult ViewCosmoseDbSource(string id, string endpoint, string key, string db, string collection)
        {
            var model = new FileSourceCodeVM();
            model.FileType = "json";
            model.FileName = id;
            // get source code 
            using (var client = new DocumentClient(new System.Uri(endpoint), key))
            {
                Microsoft.Azure.Documents.Document doc = client.ReadDocumentAsync(UriFactory.CreateDocumentUri(db, collection, id)).Result;

                model.SourceCode = doc.ToString();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteChecked([FromBody]CollectionVM[] collection)
        {

            return null;
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
