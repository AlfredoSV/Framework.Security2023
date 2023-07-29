using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using Framework.Sql2023;
using System;
using static Framework.Security2023.Entities.Login;

namespace Framework.Security2023
{
    public class ServiceLogin : IServiceLogin
    {
        public readonly SqlDB<UserFkw> _sqlDB;
        public readonly ServiceCryptography _serviceCryptography; 

        public ServiceLogin(string sqlConnection)
        {
            _serviceCryptography = new ServiceCryptography();
            //_sqlDB = new SqlDB<UserFkw>(sqlConnection);
            _sqlDB = new SqlDB<UserFkw>("server=ALFREDO ; database=Framework_Users ; integrated security = true");

        }

        public Login Login(Login userLogin)
        {
           
            UserFkw user = (new RespositorieUser().GetUser(userLogin.User));
            string passDb = string.Empty;

            if(user == null)
            {
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }

            passDb = _serviceCryptography.Descrypt(user.Password.Trim(), user.Id.ToString());

            if (passDb.Equals(userLogin.Password))
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
                newUser.Password = _serviceCryptography.Encrypt(newUser.UserName,newUser.Id.ToString());

            this._sqlDB.Insert(newUser);
            return true;
        }

        public bool DeleteUser(Guid userId)
        {
            StatusQuery rowDelete = this._sqlDB.Delete<Guid>(userId);

            return rowDelete == StatusQuery.Ok;
        }

        public bool UpdatePassword(Guid userId, string newPassword)
        {
            newPassword = _serviceCryptography.Encrypt(newPassword, userId.ToString());

            int rowUpdated = (new RespositorieUser()).UpdatePassword(userId, newPassword);

            return (rowUpdated >= 1 );
        }

        public bool UpdateUser(UserFkw user)
        {
            user.Password = _serviceCryptography.Encrypt(user.Password, user.Id.ToString());

            StatusQuery rowDelete = this._sqlDB.Update(user);

            return rowDelete == StatusQuery.Ok;
        }

        public bool UserExist(string userName)
        {
            UserFkw user = (new RespositorieUser().GetUser(userName));
            return user != null;
        }
    }
}
