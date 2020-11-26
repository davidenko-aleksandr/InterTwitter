﻿using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private List<User> _usersRepositoryMock;

        public UserService()
        {
            _usersRepositoryMock = InitData();
        }

        #region -- IUserService Implementation --

        public async Task<AOResult<List<User>>> GetUsersAsync()
        {
            var result = new AOResult<List<User>>();

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

        public async Task<AOResult<bool>> AddUserAsync(User user)
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

        public async Task<AOResult<bool>> UpdateUserAsync(User user)
        {
            var result = new AOResult<bool>();

            try
            {
                var userIndex = _usersRepositoryMock.IndexOf(user);
                _usersRepositoryMock.RemoveAt(userIndex);
                _usersRepositoryMock.Insert(userIndex, user);

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

        private List<User> InitData()
        {
            return new List<User>()
                {
                  new User()
                  {
                      Id = 0,
                      Email = "vasya1984@mail.ru",
                      Name = "Vasiliy",
                      Password = "V1984FAT",
                  },
                  new User()
                  {
                      Id = 1,
                      Email = "petya25@gmail.com",
                      Name = "Peter Stevenson",
                      Password = "Qwerty123",
                  },
                  new User()
                  {
                      Id = 2,
                      Email = "test@i.ua",
                      Name = "Test UserName",
                      Password = "Qwerty12",
                  }
               };
        }

        #endregion
    }
}