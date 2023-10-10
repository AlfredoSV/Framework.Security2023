using Framework.Security2023.Cryptography;
using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using System;

namespace Framework.Security2023.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly ServiceCryptography _serviceCryptography;
        private readonly IServiceToken _serviceToken;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceRole _serviceRole;
        private readonly IServiceEmailSecurity _serviceEmail;

        public ServiceLogin()
        {
            _serviceCryptography = new ServiceCryptography();
            _serviceToken = new ServiceToken();
            _serviceUser = new ServiceUser();
            _serviceRole = new ServiceRole();
            _serviceEmail = new ServiceEmailSecurity();
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

        public void ChangePassword(DtoChangePassword dtoChangePassword)
        {
            bool userExist = _serviceUser.UserExist(dtoChangePassword.UserName);
            UserFkw userFkw = null;
            string actualPassword = string.Empty;
            bool isUpdate = false;

            if (userExist)
            {
                userFkw = _serviceUser.GetUserByUserName(dtoChangePassword.UserName);

                actualPassword =
                   _serviceCryptography.Descrypt(userFkw.Password, userFkw.Id.ToString());

                if (dtoChangePassword.NewPassword.Equals(actualPassword))
                    throw new Exception("The password was not diferent to actual password");

                dtoChangePassword.NewPassword = _serviceCryptography.Encrypt(actualPassword, userFkw.Id.ToString());
                isUpdate = _serviceUser.UpdatePassword(userFkw.Id, dtoChangePassword.NewPassword);

                if (!isUpdate)
                    throw new Exception("The password was not updated, please contact with support");
                //else
                //    _serviceEmail.(userFkw.UserInformation.Email, string.Empty,
                //        Guid.NewGuid(), null);

            }


        }
    }
}
