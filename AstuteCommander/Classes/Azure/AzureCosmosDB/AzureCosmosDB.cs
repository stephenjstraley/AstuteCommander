using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace AstuteCommander.Classes.Azure.AzureCosmosDB
{
    public static class AzureCosmosDB
    {
        static string CURRENT_VERSION = "2017-02-22";

        public static Model.CosmosDbAccountList GetAccountList(string token, string subscriptionId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var prefix = @"https://management.azure.com/subscriptions/";
            var suffix = "/providers/Microsoft.DocumentDB/databaseAccounts";
            var uri = prefix + subscriptionId + suffix + "?api-version=2015-04-08";

            var resp = httpClient.GetAsync(uri).Result;

            if (resp.IsSuccessStatusCode)
            {
                var jsonString = resp.Content.ReadAsStringAsync().Result;

                Model.CosmosDbAccountList list = JsonConvert.DeserializeObject<Model.CosmosDbAccountList>(jsonString);

                // Pull Out Resource Group from the ID of the object
                foreach (var item in list.Items)
                {
                    var temp = item.Id.Replace(prefix, string.Empty);
                    temp = temp.Replace("subscriptions", string.Empty).Replace(suffix, string.Empty).Replace(subscriptionId, string.Empty);
                    temp = temp.Replace(@"/", string.Empty).Replace(item.Name, string.Empty).Replace("resourceGroups", string.Empty);
                    item.ResourceGroup = temp;

                    GetDbKeys(token, subscriptionId, item);
                }
                


                return list;
            }
            else
                return null;
        }
        private static void GetDbKeys(string token, string subscriptionId, Model.CosmoDbAccount db)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //            var resourceGroupName = "AHCCustomers";

            //            var uri = @"https://management.azure.com/subscriptions/" + subscriptionId + "/resourceGroups/" + resourceGroupName + "/providers/Microsoft.DocumentDB/databaseAccounts/" + db.Name + "/listKeys?api-version=2015-04-08";

            var uri = @"https://management.azure.com" + db.Id + "/listKeys?api-version=2015-04-08";

            var resp = httpClient.PostAsync(uri, null).Result;

            if (resp.IsSuccessStatusCode)
            {
                var jsonString = resp.Content.ReadAsStringAsync().Result;

                Model.CosmosDbKeys keys = JsonConvert.DeserializeObject<Model.CosmosDbKeys>(jsonString);

                db.Keys = keys;
            }

        }

        public static Model.CosmosDbAccountDbList GetDbList(string endPoint, string masterKey)
        {
            Uri baseUri = new Uri(endPoint);
            string utc_date = DateTime.UtcNow.ToString("r");

            var client = new System.Net.Http.HttpClient();
            string authHeader = string.Empty;
            string verb = string.Empty;
            string resourceType = string.Empty;
            string resourceId = string.Empty;
            string resourceLink = string.Empty;

            client.DefaultRequestHeaders.Add("x-ms-date", utc_date);
            client.DefaultRequestHeaders.Add("x-ms-version", "2017-02-22");

            //LIST all databases
            verb = "GET";
            resourceType = "dbs";
            resourceId = string.Empty;
            resourceLink = string.Format("dbs");

            authHeader = GenerateMasterKeyAuthorizationSignature(verb, resourceId, resourceType, masterKey, "master", "1.0", utc_date);

            client.DefaultRequestHeaders.Remove("authorization");
            client.DefaultRequestHeaders.Add("authorization", authHeader);

            var link = new Uri(baseUri, resourceLink);

            var resp = client.GetAsync(link).Result;

            if (resp.IsSuccessStatusCode)
            {
                var jsonString = resp.Content.ReadAsStringAsync().Result;

                Model.CosmosDbAccountDbList list = JsonConvert.DeserializeObject<Model.CosmosDbAccountDbList>(jsonString);

                return list;
            }
            else
                return null;
        }

        public static Model.CosmosDbAccountCollectionList GetDbCollectionList(string endPoint, string masterKey, string dbId)
        {
            Uri baseUri = new Uri(endPoint);
            string utc_date = DateTime.UtcNow.ToString("r");

            var client = new System.Net.Http.HttpClient();
            string authHeader = string.Empty;
            string verb = string.Empty;
            string resourceType = string.Empty;
            string resourceId = string.Empty;
            string resourceLink = string.Empty;

            client.DefaultRequestHeaders.Add("x-ms-date", utc_date);
            client.DefaultRequestHeaders.Add("x-ms-version", "2017-02-22");

            //LIST all collections in a DB
            verb = "GET";
            resourceType = "colls";
            resourceId = string.Format($"dbs/{dbId}");
            resourceLink = string.Format($"dbs/{dbId}/colls");

            authHeader = GenerateMasterKeyAuthorizationSignature(verb, resourceId, resourceType, masterKey, "master", "1.0", utc_date);

            client.DefaultRequestHeaders.Remove("authorization");
            client.DefaultRequestHeaders.Add("authorization", authHeader);

            var link = new Uri(baseUri, resourceLink);

            var resp = client.GetAsync(link).Result;

            if (resp.IsSuccessStatusCode)
            {
                var jsonString = resp.Content.ReadAsStringAsync().Result;

                Model.CosmosDbAccountCollectionList list = JsonConvert.DeserializeObject<Model.CosmosDbAccountCollectionList>(jsonString);

                return list;
            }
            else
                return null;

        }

        private static string GenerateMasterKeyAuthorizationSignature(string verb, string resourceId, string resourceType, string key, string keyType, string tokenVersion, string utcDate)
        {
            var hmacSha256 = new System.Security.Cryptography.HMACSHA256 { Key = Convert.FromBase64String(key) };

            string payLoad = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n",
                    verb.ToLowerInvariant(),
                    resourceType.ToLowerInvariant(),
                    resourceId,
                    utcDate.ToLowerInvariant(),
                    ""
            );

            byte[] hashPayLoad = hmacSha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payLoad));
            string signature = Convert.ToBase64String(hashPayLoad);

            return System.Web.HttpUtility.UrlEncode(String.Format(System.Globalization.CultureInfo.InvariantCulture, "type={0}&ver={1}&sig={2}",
                keyType,
                tokenVersion,
                signature));
        }
    }
}
