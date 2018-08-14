using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsRest
    {
        readonly IConfiguration _configuration;

        public static string _lastUri = string.Empty;

        protected HttpStatusCode _lastStatusCode;

        public VstsRest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetItem<T>()
        {
            T responseModel = default(T);

            try
            {
                var value = Request();

                if (value != null)
                {
                    responseModel = JsonConvert.DeserializeObject<T>(value);
                    TrySetProperty(responseModel, "StatusCode", _lastStatusCode);
                }

                return responseModel;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return responseModel;
            }
        }

        private void TrySetProperty(object obj, string property, object value)
        {
            var msgInfo = obj.GetType().GetProperty(property);
            msgInfo?.SetValue(obj, value, null);
        }

        protected string Request()
        {
            try
            {
                _lastStatusCode = HttpStatusCode.Unauthorized;

                using (var client = new VCHttpClient(_configuration))
                {
                    HttpResponseMessage response = client.GetResponse();

                    _lastStatusCode = response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        var tempBreak = response.Content.ReadAsStringAsync().Result;
                        return tempBreak;
                    }
                    else
                        return null;
               }
            }
            catch
            {
                return null;
            }
        }
    }
}
