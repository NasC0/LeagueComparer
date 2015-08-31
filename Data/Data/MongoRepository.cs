﻿using System;
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
        private const string IdFieldName = "_id";

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

        public async Task Replace(T element)
        {
            var filter = this.GetIdFilterDefinition(element);

            await this.collection.ReplaceOneAsync(filter, element);
        }

        public async Task Replace(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                var filter = this.GetIdFilterDefinition(element);
                await this.collection.ReplaceOneAsync(filter, element);
            }
        }

        public async Task Update(T element, UpdateDefinition<T> updateDefinition)
        {
            var filter = this.GetIdFilterDefinition(element);
            await this.collection.UpdateOneAsync(filter, updateDefinition);
        }

        public async Task<T> Remove(T element)
        {
            var filter = this.GetIdFilterDefinition(element);
            await this.collection.DeleteOneAsync(filter);
            return element;
        }

        public async Task<IEnumerable<T>> Remove(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                var filter = this.GetIdFilterDefinition(element);
                await this.collection.DeleteOneAsync(filter);
            }

            return elements;
        }

        private FilterDefinition<T> GetIdFilterDefinition(T element)
        {
            var elementId = element.GetValue(IdFieldName);
            var filter = this.filterDefinitionBuilder.Eq(x => x.GetValue(IdFieldName), elementId);
            return filter;
        }
    }
}
