using System.Configuration;
using Core.Interfaces;

namespace AuthServer
{
    public class WebApplicatonConfig : IWebApplicationConfig
    {
        public string MongoDbName { get; }
        public string MongoDbConnectionString { get; }
        public string SqlDbName => ConfigurationManager.ConnectionStrings["SqlUsersResourceServer"].ConnectionString;
    }
}