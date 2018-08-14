using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AstuteCommander.Models
{
    public class CosmosAccountsVM
    {
        public string AccountId { get; set; }
        public string Endpoint { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string PrimaryMasterKey { get; set; }
        public string SecondaryMasterKey { get; set; }
    }

    public class CosmosDbAccountDbVM
    {
        public string DbAccountName { get; set; }
        public string DbEndpoint { get; set; }
        public string DbPrimaryMasterKey { get; set; }
        public string DbName { get; set; }
    }

    public class CosmosDbAccountDbCollectionVM
    {
        public string DbColAccountName { get; set; }
        public string DbColEndpoint { get; set; }
        public string DbColPrimaryMasterKey { get; set; }
        public string ColName { get; set; }
        public string DbColName { get; set; }
    }
    public class CollectionVM
    {
        public string ActualEndpoint { get; set; }
        public string ActualPrimaryMasterKey { get; set; }
        public string ActualColName { get; set; }
        public string ActualDbName { get; set; }
        public string Id { get; set; }
        public string GatewayTimestamp { get; set; }
        public string HubId { get; set; }
        public string DeviceType { get; set; }
        public string RawData { get; set; }
        public bool Delete { get; set; }
    }
}
