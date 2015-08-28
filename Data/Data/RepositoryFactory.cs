using System;
using System.Collections.Generic;
using Data.Contracts;
using Helpers;
using Models.Common;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IMongoDatabase mongoDatabase;
        private IDictionary<Type, object> repositories;

        public RepositoryFactory(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
            this.repositories = new Dictionary<Type, object>();
        }

        public IMongoRepository<T> GetRepository<T>() where T : Entity
        {
            return this.GetSavedRepository<T>();
        }

        private IMongoRepository<T> GetSavedRepository<T>() where T : Entity
        {
            var objectType = typeof(T);
            if (!this.repositories.ContainsKey(objectType))
            {
                var collectionName = GetCollectionStringName.GetCollectionName<T>();
                var repoCollection = this.mongoDatabase.GetCollection<T>(collectionName);
                var currentRepository = Activator.CreateInstance(typeof(MongoRepository<T>), repoCollection);
                this.repositories.Add(objectType, currentRepository);
            }

            var currentRepo = this.repositories[objectType] as IMongoRepository<T>;
            return currentRepo;
        }
    }
}
