using System.Configuration;
using Core.Interfaces;

namespace ResourceServer
{
    public class WebApplicationConfig  : IWebApplicationConfig
    {
        public string MongoDbName => ConfigurationManager.AppSettings["MongoDbName"];
        public string MongoDbConnectionString => ConfigurationManager.AppSettings["MongoDbConnectionString"].Replace("{DB_NAME}", MongoDbName);
        public string SqlDbName { get; }
    }
}