using Microsoft.Extensions.Configuration;

namespace AstuteCommander
{
    public interface IAppSettings
    {
        IConfiguration Setting { get; set; }
        string AzureActiveDirectoryInstance { get; }
        string Tenant { get; }
        string ClientId { get; }
        string AppKey { get; }
        string Resource { get; }
        string Authority { get; }
        string SubscriptionId { get; }
    }
    class AppSettings : IAppSettings
    {
        public IConfiguration Setting { get; set; }

        public AppSettings(IConfiguration parm)
        {
            Setting = parm;
        }

        public string AzureActiveDirectoryInstance => Setting["AzureActiveDirectoryInstance"];
        public string Tenant => Setting["Tenant"];
        public string ClientId => Setting["ClientId"];
        public string AppKey => Setting["AppKey"];
        public string Resource => Setting["Resource"];
        public string Authority => AzureActiveDirectoryInstance + Tenant;
        public string SubscriptionId => Setting["SubscriptionId"];
    }
}
