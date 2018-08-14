using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


namespace AstuteCommander.Classes.Azure.AzureCosmosDB
{
    public class AzureCosmosDBRepository<T> where T : class, IDisposable
    {
        private static DocumentClient client;

        public static void Initialize(string endPoint, string key)
        {
            client = new DocumentClient(new Uri(endPoint), key);
//            CreateDatabaseIfNotExistsAsync().Wait();
//            CreateCollectionIfNotExistsAsync().Wait();
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(string db, string collection)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(db, collection),
                new FeedOptions { MaxItemCount = -1 })
//                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }


    }
}
