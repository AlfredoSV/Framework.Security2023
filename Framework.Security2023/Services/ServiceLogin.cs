﻿using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;

namespace Framework.Security2023.Services
{
    public class ServiceLogin : IServiceLogin
    {   
        private readonly ServiceCryptography _serviceCryptography;
        private readonly IServiceToken _serviceToken;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceRole _serviceRole;
        
        public ServiceLogin()
        {
            _serviceCryptography = new ServiceCryptography();
            _serviceToken = new ServiceToken();
            _serviceUser = new ServiceUser();
            _serviceRole = new ServiceRole();
    
        }

        public Login LoginDummy(Login userLogin)
        {
            userLogin.StatusLog = StatusLogin.Ok;

            return userLogin;
        }


        public Login Login(Login userLogin)
        {
           
            UserFkw user = (_serviceUser.GetUserByUserName(userLogin.UserName));
            string passDb = string.Empty;
   
            if (user is null)
            {
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }
   
            passDb = _serviceCryptography.Descrypt(user.Password.Trim(), user.Id.ToString());

            if (!passDb.Equals(userLogin.Password))
            {
                _serviceUser.SaveUserLoginAttempt(user.Id,
                        "PasswordIncorrect");
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }

            _serviceUser.UpdateStatusBlocked(user.Id);

            if (user.UserBlocked)
            {
                userLogin.StatusLog = StatusLogin.UserBlocked;
                return userLogin;
            }
                

            if (user.LoginSessions >= 1)
            {
                userLogin.StatusLog = StatusLogin.ExistSession;
                return userLogin;
            }
                
            if (user.ApplyToken)           
                _serviceToken.CreateToken(user);

            user.Role = _serviceRole.GetRole(user.Id);
            userLogin.User = user;

            _serviceUser.UpdateLoginSessions(user.Id);

            return userLogin;
        }

    }
}
