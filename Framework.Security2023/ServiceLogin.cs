using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;

namespace Framework.Security2023
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

            if(user == null)
            {
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }

            passDb = _serviceCryptography.Descrypt(user.Password.Trim(), user.Id.ToString());

            if (!passDb.Equals(userLogin.Password))
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;

            if (user.UserBlocked)
                userLogin.StatusLog = StatusLogin.UserBlocked;

            if (user.LoginSessions >=1)
                userLogin.StatusLog = StatusLogin.ExistSession;

            if (user.ApplyToken)           
                _serviceToken.CreateToken(user);

            user.Role = _serviceRole.GetRole(user.Id);
            userLogin.User = user;

            return userLogin;
        }

    }
}
