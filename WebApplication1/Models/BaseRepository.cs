using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
{
    public class BaseRepository<T> where T : IHasId
    {
        private IMongoCollection<T> collection;

        public BaseRepository(AppConfig appConfig)
        {
            var db = new MongoClient(appConfig.MongoConnection.ConnectionString);

            var edb = db.GetDatabase(appConfig.MongoConnection.DatabaseName);

            collection = edb.GetCollection<T>(typeof(T).Name);
        }

        public void Create(T user)
        {
            collection.InsertOne(user);
        }

        public T Get(ObjectId id)
        {
            var cursor = collection.FindSync(x => x.Id == id);
            return cursor.FirstOrDefault();
        }

        public T FindOne(Expression<Func<T, bool>> exp)
        {
            var cursor = collection.FindSync(exp);
            return cursor.FirstOrDefault();
        }

        public void Update(T user)
        {
            collection.ReplaceOne(x => x.Id == user.Id, user);
        }

        public List<T> GetMany()
        {
            //var query = collection.AsQueryable().Where(x => x.Tname == "login");

            //return query.ToList();

            return collection.FindSync(Builders<T>.Filter.Empty).ToList();

            //return collection.FindSync(x => true).ToList();
        }
    }
}
