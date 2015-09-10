using System;
using System.Collections.Generic;
using Data.Contracts;
using Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class RepositoryFactory : IRepositoryFactory, IDisposable
    {
        private IMongoDatabase mongoDatabase;
        private IDictionary<Type, object> repositories;

        public RepositoryFactory(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>()
        {
            return this.GetSavedRepository<T>();
        }

        public IRepository<T> GetRepository<T>(string collectionName)
        {
            return this.GetSavedRepository<T>(collectionName);
        }

        public IRepository<BsonDocument> GetRepository(string collectionName)
        {
            var repoCollection = this.mongoDatabase.GetCollection<BsonDocument>(collectionName);
            var genericRepository = new MongoRepository<BsonDocument>(repoCollection);

            return genericRepository;
        }

        private IRepository<T> GetSavedRepository<T>(string collectionName = null)
        {
            var objectType = typeof(T);
            if (!this.repositories.ContainsKey(objectType))
            {
                if (collectionName.IsInvalid())
                {
                    collectionName = GetCollectionStringName.GetCollectionName<T>();
                }

                var repoCollection = this.mongoDatabase.GetCollection<T>(collectionName);
                var currentRepository = Activator.CreateInstance(typeof(MongoPocoRepository<T>), repoCollection);
                this.repositories.Add(objectType, currentRepository);
            }

            var currentRepo = this.repositories[objectType] as IRepository<T>;
            return currentRepo;
        }

        public void Dispose()
        {
            this.mongoDatabase = null;
            this.repositories = null;
        }
    }
}
