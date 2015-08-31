using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Contracts;
using Models.Common;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class MongoPocoRepository<T> : IMongoRepository<T> where T : Entity
    {
        private IMongoCollection<T> collection;

        public MongoPocoRepository(IMongoCollection<T> collection)
        {
            this.collection = collection;
        }

        public async Task<IEnumerable<T>> All()
        {
            var filterDocument = new BsonDocument();
            var documents = await this.collection.FindAsync<T>(filterDocument);
            return await documents.ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            var filteredDocuments = await this.collection.FindAsync(expression);
            var documentsList = await filteredDocuments.ToListAsync();
            return documentsList;
        }

        public async Task<IEnumerable<T>> Find(FilterDefinition<T> filter)
        {
            var filteredDocuments = await this.collection.FindAsync(filter);
            var documentsList = await filteredDocuments.ToListAsync();
            return documentsList;
        }

        public async Task Add(T element)
        {
            await this.collection.InsertOneAsync(element);
        }

        public async Task Add(IEnumerable<T> elements)
        {
            foreach (var item in elements)
            {
                await this.collection.InsertOneAsync(item);
            }
        }

        public async Task Replace(T element, Expression<Func<T, bool>> matchElement)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            await this.collection.ReplaceOneAsync<T>(matchElement, element);
        }

        public async Task Replace(IEnumerable<T> elements, Expression<Func<T, bool>> matchElements)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            foreach (var item in elements)
            {
                await this.collection.ReplaceOneAsync<T>(matchElements, item, updateOptions);
            }
        }

        public async Task Update(T element, Expression<Func<T, bool>> matchElements, UpdateDefinition<T> updateDefinition)
        {
            await this.collection.UpdateOneAsync<T>(matchElements, updateDefinition);
        }

        public async Task<T> Remove(T element, Expression<Func<T, bool>> matchElements)
        {
            await this.collection.DeleteOneAsync<T>(matchElements);
            return element;
        }

        public async Task<IEnumerable<T>> Remove(IEnumerable<T> elements, Expression<Func<T, bool>> matchElements)
        {
            foreach (var item in elements)
            {
                await this.collection.DeleteOneAsync<T>(matchElements);
            }

            return elements;
        }
    }
}
