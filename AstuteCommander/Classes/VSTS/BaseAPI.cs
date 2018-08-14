using System.Net;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public abstract class BaseAPI
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
