using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class MongoRepository<T> : IRepository<T> where T: BsonDocument
    {
        private IMongoCollection<T> collection;
        private FilterDefinitionBuilder<T> filterDefinitionBuilder;

        public MongoRepository(IMongoCollection<T> collection)
        {
            this.collection = collection;
            this.filterDefinitionBuilder = new FilterDefinitionBuilder<T>();
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

        public async Task Replace(T element, Expression<Func<T, bool>> expression)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            await this.collection.ReplaceOneAsync(expression, element, updateOptions);
        }

        public async Task Replace(IEnumerable<T> elements, Expression<Func<T, bool>> expression)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            foreach (var element in elements)
            {
                await this.collection.ReplaceOneAsync(expression, element, updateOptions);
            }
        }

        public async Task Update(T element, Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            await this.collection.UpdateOneAsync(expression, updateDefinition);
        }

        public async Task<T> Remove(T element, Expression<Func<T, bool>> expression)
        {
            await this.collection.DeleteOneAsync(expression);
            return element;
        }

        public async Task<IEnumerable<T>> Remove(IEnumerable<T> elements, Expression<Func<T, bool>> expression)
        {
            foreach (var element in elements)
            {
                await this.collection.DeleteOneAsync(expression);
            }

            return elements;
        }
    }
}
