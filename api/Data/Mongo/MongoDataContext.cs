using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Data.Mongo
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database;
        private MongoRepository<Event> _events;

        public MongoDataContext(IWebApplicationConfig config) : this(config.MongoDbConnectionString, config.MongoDbName)
        { }

        public MongoDataContext(string mongoDbConnectionString, string mongoDbName)
        {
            var client = new MongoClient(mongoDbConnectionString);
            _database = client.GetDatabase(mongoDbName);
        }

        public IRepository<Event> Events => _events ?? (_events = new MongoRepository<Event>(_database));
    }
}
