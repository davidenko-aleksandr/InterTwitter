using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.Reposytory
{
    public class RepositoryMock : IRepository
    {
        private List<User> _users;
        public RepositoryMock()
        {
           
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : class, IEntity, new()
        {
            return _users as List<T>;
        }

        public async Task<int> AddOrrUpdateAsync<T>(T item) where T : class, IEntity, new()
        {
            int id;
            var user = item as User;
            if (user != null)
            {
                _users.Add(user);
                id = _users.IndexOf(user);
            }
            else
            {
                id = -1;
            }
                return id;
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : class, IEntity, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new()
        {
            throw new NotImplementedException();
        }        
    }
}
