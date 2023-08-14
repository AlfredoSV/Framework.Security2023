using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using static Framework.Security2023.Entities.Login;

namespace Framework.Security2023
{
    public class ServiceLogin : IServiceLogin
    {   
        public readonly ServiceCryptography _serviceCryptography; 

        public ServiceLogin(string sqlConnection)
        {
            _serviceCryptography = new ServiceCryptography();
    
        }

        public Login LoginDummy(Login userLogin)
        {
            userLogin.StatusLog = StatusLogin.Ok;

            return userLogin;
        }


        public Login Login(Login userLogin)
        {
           
            UserFkw user = (new RespositoryUser().GetUser(userLogin.User));
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

            if (user.ApplyToken)
            {
                if(user.UserToken != null)
                {
                    //Logic for validation of token
                }
                else
                    userLogin.StatusLog = StatusLogin.TokenNotValid;
                
            }

            return userLogin;
        }

        public bool CreateUser(UserFkw newUser,bool isCreatedByAdmin)
        {
            if (isCreatedByAdmin)
                newUser.SetPassword(_serviceCryptography.Encrypt(newUser.UserName,newUser.Id.ToString()));

            (new RespositoryUser()).Save(newUser);
            return true;
        }

        public bool DeleteUser(Guid userId)
        {
            int rowDelete = (new RespositoryUser()).Delete(userId);

            return rowDelete > 0;
        }

        public bool UpdatePassword(Guid userId, string newPassword)
        {
            newPassword = _serviceCryptography.Encrypt(newPassword, userId.ToString());

            int rowUpdated = (new RespositoryUser()).UpdatePassword(userId, newPassword);

            return (rowUpdated >= 1 );
        }

        public bool UpdateUser(UserFkw user)
        {
            user.SetPassword(_serviceCryptography.Encrypt(user.Password, user.Id.ToString()));

            int res = (new RespositoryUser()).Update(user);

            return res > 0;
        }

        public bool UserExist(string userName)
        {
            UserFkw user = (new RespositoryUser().GetUser(userName));
            return user != null; 
        }
    }
}
