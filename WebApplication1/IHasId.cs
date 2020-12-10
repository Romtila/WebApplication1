using MongoDB.Bson;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
{
    public interface IHasId
    {
        ObjectId Id { get; set; }
    }
}
