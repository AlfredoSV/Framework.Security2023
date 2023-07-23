﻿using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using Framework.Sql2023;
using System;
using static Framework.Security2023.Entities.Login;

namespace Framework.Security2023
{
    public class ServiceLogin : IServiceLogin
    {
        public readonly SqlDB<UserFkw> _sqlDB;

        public ServiceLogin()
        {
            _sqlDB = new SqlDB<UserFkw>("server=ALFREDO ; database=Framework_Users ; integrated security = true");

        }

        public Login Login(Login userLogin)
        {
           
            UserFkw user = (new RespositorieUser().GetUser(userLogin.User));

            if(user == null)
            {
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }

            if (user.Password.Trim().Equals(userLogin.Password))
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;

            if (user.UserBlocked)
                userLogin.StatusLog = StatusLogin.UserBlocked;

            if (user.LoginSessions >=1)
                userLogin.StatusLog = StatusLogin.ExistSession;

            return userLogin;
        }

        public bool CreateUser(UserFkw newUser,bool isCreatedByAdmin)
        {
            if (isCreatedByAdmin)
                newUser.Password = newUser.UserName;

            //int rowsRegister = (new RespositorieUser().Save(newUser));        
            this._sqlDB.Insert(newUser);
            return true;
        }

        public bool DeleteUser(Guid userId)
        {
            //int rowsRegister = (new RespositorieUser().Delete(userId));

            StatusQuery rowDelete = this._sqlDB.Delete<Guid>(userId);

            return rowDelete == StatusQuery.Ok;
        }

        public bool UpdatePassword(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserFkw user)
        {
            StatusQuery rowDelete = StatusQuery.Ok;

            return rowDelete == StatusQuery.Ok;
        }
    }
}
