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
            _users = new List<User>()
            {
                new User()
                {
                    Id = 0,
                    Email = "vasya1984@mail.ru",
                    Name = "Vasiliy",
                    Password = "v1984!",
                },
                new User()
                {
                    Id = 1,
                    Email = "petya25@gmail.com",
                    Name = "Peter Stevenson",
                    Password = "qwerty123",
                }
            };
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

        public async Task<List<T>> GetItemsAsync<T>() where T : class, IEntity, new()
        {
            return _users as List<T>;
        }
    }
}
