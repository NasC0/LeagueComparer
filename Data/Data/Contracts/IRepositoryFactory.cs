using System;
using Models.Common;
using MongoDB.Bson;

namespace Data.Contracts
{
    public interface IRepositoryFactory : IDisposable
    {
        IRepository<T> GetRepository<T>();

        IRepository<T> GetRepository<T>(string collectionName);

        IRepository<BsonDocument> GetRepository(string collectionName);
    }
}
