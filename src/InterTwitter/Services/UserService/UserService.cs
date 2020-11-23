using InterTwitter.Models;
using InterTwitter.Services.Reposytory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public UserService(IRepository repository,
                            ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region --IUserServiceImplementation--

        public async Task<int> AddOrUpdateAsync(User user)
        {
            return await _repository.AddOrrUpdateAsync(user);
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            return await _repository.DeleteItemAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetItemsAsync<User>();
        }
        #endregion
    }
}
