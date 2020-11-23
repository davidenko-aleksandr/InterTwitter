using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.Reposytory
{
    public interface IRepository
    {
        Task<List<T>> GetItemsAsync<T>() where T : class, IEntity, new();

        Task<int> DeleteItemAsync<T>(T item) where T : class, IEntity, new();

        Task<int> AddOrrUpdateAsync<T>(T item) where T : class, IEntity, new();

        Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new();
    }
}
