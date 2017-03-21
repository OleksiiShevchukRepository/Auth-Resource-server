namespace Core.Interfaces
{
    public interface IWebApplicationConfig
    {
        string MongoDbName { get; }
        string MongoDbConnectionString { get; }
        string SqlDbName { get; }
        string SqlDbConnectionString { get; }
    }
}