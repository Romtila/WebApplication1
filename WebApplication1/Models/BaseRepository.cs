using MongoDB.Driver;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
{
    public class BaseRepository<T> where T : IHasId
    {
        private IMongoCollection<T> collection;

        public BaseRepository()
        {
            var db = new MongoClient("mongodb://localhost:27017");

            var edb = db.GetDatabase("WebAppDB");

            collection = edb.GetCollection<T>(nameof(T));
        }
    }
}
