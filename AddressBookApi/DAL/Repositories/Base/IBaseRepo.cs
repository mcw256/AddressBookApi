using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressBookApi.DAL.Repositories.Base
{
    public interface IBaseRepo<TModel> where TModel : class
    {
        public Task<IEnumerable<TModel>> Find(Expression<Func<TModel, bool>> filter);
        public Task<IEnumerable<TModel>> FindWithPaging(Expression<Func<TModel, bool>> filter, int pageNo, int pageSize);
        public Task<long> Count(Expression<Func<TModel, bool>> filter);
        public Task<TModel> FindOne(Expression<Func<TModel, bool>> filter);
        public Task<TModel> AddOne(TModel item);
        public Task UpdateOne(Expression<Func<TModel, bool>> filter, TModel item);
        public Task DeleteOne(Expression<Func<TModel, bool>> filter);
    }
}
