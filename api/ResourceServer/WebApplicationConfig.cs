using Core.Interfaces;

namespace ResourceServer
{
    public class WebApplicationConfig : IWebApplicationConfig
    {
        public string MongoDbName { get; }
        public string MongoDbConnectionString { get; }
        public string SqlDbName { get; }
        public string SqlDbConnectionString { get; }
    }
}