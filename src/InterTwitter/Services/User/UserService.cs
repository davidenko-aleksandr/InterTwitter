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
            InitData();
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
                result.SetError($"{nameof(AddUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> UpdateUserAsync(UserViewModel userViewModel)
        {
             
            var result = new AOResult<bool>();

            try
            {
                var changingUser = _usersRepositoryMock.FirstOrDefault(x => x.Id == user.Id);

                if (changingUser is not null)
                {
                    changingUser.Avatar = user.Avatar;
                    changingUser.Email = user.Email;
                    changingUser.ProfileHeaderImage = user.ProfileHeaderImage;
                    changingUser.Name = user.Name;
                    changingUser.Password = user.Password;

                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(UpdateUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private Helpers -- 

        private void InitData()
        {
            _usersRepositoryMock = new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 0,
                    Email = "trump@mail.ru",
                    Name = "Donald J. Trump",
                    Password = "Trump20",
                    Avatar = "https://pbs.twimg.com/profile_images/874276197357596672/kUuht00m_400x400.jpg",
                    ProfileHeaderImage = "https://pbs.twimg.com/profile_banners/25073877/1604214583/1500x500",
                },
                new UserModel()
                {
                    Id = 1,
                    Email = "shakira@gmail.com",
                    Name = "Shakira",
                    Password = "Shakira20",
                    Avatar = "https://pbs.twimg.com/profile_images/1298649731980238848/29o9j4e__400x400.jpg",
                    ProfileHeaderImage = "https://pbs.twimg.com/profile_banners/44409004/1600521595/1500x500",
                },
                new UserModel()
                {
                    Id = 2,
                    Email = "test@i.ua",
                    Name = "Test UserName",
                    Password = "Qwerty12",
                    Avatar = Constants.DefaultProfilePicture,
                    ProfileHeaderImage = "",
                },
                new UserModel()
                {
                    Id = 3,
                    Email = "xamarin@bugs.net",
                    Name = "Xamarin",
                    Password = "Xamarin20",
                    Avatar = "https://pbs.twimg.com/profile_images/471641515756769282/RDXWoY7W_400x400.png",
                    ProfileHeaderImage = "https://pbs.twimg.com/profile_banners/299659914/1401283128/1500x500",
                }
            };

        }

        #endregion

    }
}
