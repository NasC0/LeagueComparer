using Models.Common;
using MongoDB.Bson;

namespace Data.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : Entity;

        IRepository<BsonDocument> GetRepository(string name);
    }
}
