using Infrastructure.Data.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Data.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Document> Documents => _database.GetCollection<Document>("Documents");
    }
}
