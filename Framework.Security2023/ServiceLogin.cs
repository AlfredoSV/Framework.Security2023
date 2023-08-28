using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using static Framework.Security2023.Entities.Login;

namespace Framework.Security2023
{
    public class ServiceLogin : IServiceLogin
    {   
        private readonly ServiceCryptography _serviceCryptography;
        private readonly IServiceToken _serviceToken;
        private readonly ServiceUser _serviceUser;
        private readonly RespositoryUser _respositoryUser;
        
        public ServiceLogin()
        {
            _serviceCryptography = new ServiceCryptography();
            _serviceToken = new ServiceToken();
            _serviceUser = new ServiceUser();
            _respositoryUser = new RespositoryUser();
    
        }

        public Login LoginDummy(Login userLogin)
        {
            userLogin.StatusLog = StatusLogin.Ok;

            return userLogin;
        }


        public Login Login(Login userLogin)
        {
           
            UserFkw user = (_respositoryUser.GetUser(userLogin.UserName));
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
                _serviceToken.CreateToken(user);

            user.SetRole(_serviceUser.GetRole(user.Id));
            userLogin.User = user;

            return userLogin;
        }

    }
}
