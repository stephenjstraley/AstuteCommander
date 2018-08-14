using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AstuteCommander.Classes
{
    public class VCHttpClient : HttpClient
    {
        protected string _additionalApi = string.Empty;

        public VCHttpClient(IConfiguration config) : base()
        {
            BaseAddress = new Uri(config.UriString);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.Credentials);
        }

        public VCHttpClient(IConfiguration config, string uri) : base()
        {
            BaseAddress = new Uri(uri);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.Credentials);
        }

        public string AdditionalApi
        {
            set { _additionalApi = value; }
        }

        public HttpResponseMessage GetResponse()
        {
            return base.GetAsync(_additionalApi).Result;
        }
    }
}
