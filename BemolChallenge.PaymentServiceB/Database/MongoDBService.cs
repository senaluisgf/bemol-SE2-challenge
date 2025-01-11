using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BemolChallenge.PaymentServiceB.Database
{
    public class MongoDBService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public MongoDBService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration["ConnectionStrings:DbConnection"];
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}
