using AddressBookApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories.Base
{
    public class BaseRepo<TModel> : IBaseRepo<TModel> where TModel : class
    {
        private readonly IMongoCollection<TModel> _collection;

        public BaseRepo(IMongoClient mongoClient, IMongoDbSettings mongoDbSettings)
        {
            var dbName = mongoDbSettings.DatabaseName;
            var collectionName = mongoDbSettings.CollectionName;
            
            var database = mongoClient.GetDatabase(dbName);
            _collection = database.GetCollection<TModel>(collectionName);
        }

        public async Task<IEnumerable<TModel>> Find(Expression<Func<TModel, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> FindWithPaging(Expression<Func<TModel, bool>> filter, int pageNo, int pageSize)
        {
            return await _collection.Find(filter)
                .Skip((pageNo - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<long> Count(Expression<Func<TModel, bool>> filter)
        {
            return await _collection.Find(filter).CountDocumentsAsync();
        }

        public async Task<TModel> FindOne(Expression<Func<TModel, bool>> filter)
        {
            return (await _collection.Find(filter).ToListAsync()).FirstOrDefault();
        }

        public async Task<TModel> AddOne(TModel item)
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public async Task UpdateOne(Expression<Func<TModel, bool>> filter, TModel item)
        {
            await _collection.ReplaceOneAsync(filter, item);
        }


        public async Task DeleteOne(Expression<Func<TModel, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }

       
    }
}
