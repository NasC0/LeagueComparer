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
    public class MongoRepository<T> : IMongoRepository<T> where T : Entity
    {
        private IMongoCollection<T> collection;

        public MongoRepository(IMongoCollection<T> collection)
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

        public async Task Replace(T element)
        {
            await this.collection.ReplaceOneAsync<T>(x => x.Id == element.Id, element);
        }

        public async Task Replace(IEnumerable<T> elements)
        {
            foreach (var item in elements)
            {
                await this.collection.ReplaceOneAsync<T>(x => x.Id == item.Id, item);
            }
        }

        public async Task Update(T element, UpdateDefinition<T> updateDefinition)
        {
            await this.collection.UpdateOneAsync<T>(x => x.Id == element.Id, updateDefinition);
        }

        public async Task<T> Remove(T element)
        {
            await this.collection.DeleteOneAsync<T>(x => x.Id == element.Id);
            return element;
        }

        public async Task<IEnumerable<T>> Remove(IEnumerable<T> elements)
        {
            foreach (var item in elements)
            {
                await this.collection.DeleteOneAsync<T>(x => x.Id == item.Id);
            }

            return elements;
        }
    }
}
