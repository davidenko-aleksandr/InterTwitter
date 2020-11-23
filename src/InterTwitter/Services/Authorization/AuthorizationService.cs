using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Reposytory;
using InterTwitter.Services.UserService;

namespace InterTwitter.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private List<User> _usersRepositoryMock;

        private readonly IUserService _userService;

        public AuthorizationService(IUserService repository)
        {
            _userService = repository;

            //_usersRepositoryMock = new List<User>()
            //{
            //    new User()
            //    {
            //        Id = 0,
            //        Email = "vasya1984@mail.ru",
            //        Name = "Vasiliy",
            //        Password = "v1984!",
            //    },
            //    new User()
            //    {
            //        Id = 1,
            //        Email = "petya25@gmail.com",
            //        Name = "Peter Stevenson",
            //        Password = "qwerty123",
            //    },
            //};
        }

        #region -- IAuthorizationService Implementation --

        public async Task<AOResult<bool>> LogInAsync(string email, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var users = await _userService.GetUsersAsync();
                var user = users.First(x => x.Email == email && x.Password == password);
                //var user = _usersRepositoryMock.First(x => x.Email == email && x.Password == password);
                await Task.Delay(300);
                if (user != null)
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(LogInAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> SignUpAsync(string email, string name, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var users = await _userService.GetUsersAsync();
                var user = users.First(x => x.Email == email && x.Password == password);
                //  var user = _usersRepositoryMock.First(x => x.Email == email);
                await Task.Delay(300);
                if (user == null)
                {
                    await _userService.AddOrUpdateAsync(new User()
                    {
                        Email = email,
                        Name = name,
                        Password = password,
                    });
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(SignUpAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
