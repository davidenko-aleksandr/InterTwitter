using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Settings;

namespace InterTwitter.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private List<User> _usersRepositoryMock;
        private readonly ISettingsService _settingsService;

        public AuthorizationService(ISettingsService settingsService)
        {
            _usersRepositoryMock = new List<User>()
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
                },
            };

            _settingsService = settingsService;
        }

        #region -- IAuthorizationService Implementation --

        public bool IsAuthorized
        {
            get => _settingsService.UserEmail != string.Empty;
        }


        public async Task<AOResult<bool>> LogInAsync(string email, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var user = _usersRepositoryMock.First(x => x.Email == email && x.Password == password);
                await Task.Delay(300);

                if (user != null)
                {
                    _settingsService.UserEmail = user.Email;
  
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
                var user = _usersRepositoryMock.First(x => x.Email.ToUpper() == email.ToUpper());
                await Task.Delay(300);
                if (user == null)
                {
                    _usersRepositoryMock.Add(new User()
                    {
                        Id = _usersRepositoryMock.Count,
                        Email = email,
                        Name = name,
                        Password = password,
                    });

                    _settingsService.UserEmail = email;

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

        public void LogOut()
        {
            _settingsService.ClearData();
        }

        #endregion
    }
}
