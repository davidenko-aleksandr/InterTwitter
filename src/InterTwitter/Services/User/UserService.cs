using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Extensions;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private List<UserModel> _usersRepositoryMock;

        public UserService()
        {
            _usersRepositoryMock = InitData();
        }

        #region -- IUserService Implementation --

        public async Task<AOResult<List<UserViewModel>>> GetUsersAsync()
        {
            var result = new AOResult<List<UserViewModel>>();

            try
            {
                if (_usersRepositoryMock != null)
                {
                    var list = _usersRepositoryMock.Select(x => new UserViewModel(x)).ToList();
                    result.SetSuccess(list);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUsersAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetUserAsync(int id)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var user = _usersRepositoryMock.First(x => x.Id == id);

                if (user != null)
                {
                    result.SetSuccess(user);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> AddUserAsync(UserModel user)
        {
            var result = new AOResult<bool>();
            try
            {
                _usersRepositoryMock.Add(user);
                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.SetFailure(false);
                result.SetError($"{nameof(AddUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> UpdateUserAsync(UserViewModel userViewModel)
        {
             
            var result = new AOResult<bool>();

            try
            {
                var userMock = _usersRepositoryMock.Where(x => x.Id == userViewModel.Id).First();
                //userMock = userViewModel.ToUserModel(); 

                var userIndex = _usersRepositoryMock.IndexOf(userMock);
                _usersRepositoryMock.RemoveAt(userIndex);
                _usersRepositoryMock.Insert(userIndex, userViewModel.ToUserModel());

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.SetFailure(false);
                result.SetError($"{nameof(UpdateUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private Helpers -- 

        private List<UserModel> InitData()
        {
            return new List<UserModel>()
                {
                  new UserModel()
                  {
                      Id = 0,
                      Email = "vasya1984@mail.ru",
                      Name = "Vasiliy",
                      Password = "V1984FAT",
                      Picture = Constants.DefaultProfilePicture,
                  },
                  new UserModel()
                  {
                      Id = 1,
                      Email = "petya25@gmail.com",
                      Name = "Peter Stevenson",
                      Password = "Qwerty123",
                      Picture = Constants.DefaultProfilePicture,
                  },
                  new UserModel()
                  {
                      Id = 2,
                      Email = "test@i.ua",
                      Name = "Test UserName",
                      Password = "Qwerty12",
                      Picture = Constants.DefaultProfilePicture,
                  }
               };
        }

        #endregion
    }
}
