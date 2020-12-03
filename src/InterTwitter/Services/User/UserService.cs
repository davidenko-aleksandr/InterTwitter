using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<AOResult<IEnumerable<UserModel>>> GetUsersAsync()
        {
            var result = new AOResult<IEnumerable<UserModel>>();

            try
            {
                if (_usersRepositoryMock != null)
                {
                    result.SetSuccess(_usersRepositoryMock);
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
                var user = _usersRepositoryMock.FirstOrDefault(x => x.Id == id);

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

        public async Task<AOResult<bool>> UpdateUserAsync(UserModel userModel)
        {
            var result = new AOResult<bool>();

            try
            {
                var changingUser = _usersRepositoryMock.FirstOrDefault(x => x.Id == userModel.Id);

                if (changingUser != null)
                {
                    changingUser.Avatar = userModel.Avatar;
                    changingUser.Email = userModel.Email;
                    changingUser.ProfileHeaderImage = userModel.ProfileHeaderImage;
                    changingUser.Name = userModel.Name;
                    changingUser.Password = userModel.Password;

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
                    ProfileHeaderImage = "pic_profile_header_photo.jpg"
                },
                new UserModel()
                {
                    Id = 3,
                    Email = "xamarin@bugs.net",
                    Name = "Xamarin",
                    Password = "Xamarin20",
                    Avatar = "https://camo.githubusercontent.com/c44a0cd002d743f55c5cace3716bd2dc87055d796fd681af90d564d669d5b27c/68747470733a2f2f672e7265646469746d656469612e636f6d2f46326d53715263654e5162596457684161546f307879347552345178516c424d524659416e3178724b4b342e6769663f773d33323026733d6232643165353665383237333333373130343861376532623664363162376638",
                    ProfileHeaderImage = "https://pbs.twimg.com/profile_banners/299659914/1401283128/1500x500",
                },
            };
        }

        #endregion

    }
}
