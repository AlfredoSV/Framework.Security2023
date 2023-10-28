using Framework.Security2023.Cryptography;
using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
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
        private readonly RepositoryChangePasswordRequest _changePasswordRequestRepo;

        public ServiceLogin()
        {
            _changePasswordRequestRepo = new RepositoryChangePasswordRequest();
            _serviceCryptography = new ServiceCryptography();
            _serviceToken = new ServiceToken();
            _serviceUser = new ServiceUser();
            _serviceRole = new ServiceRole();
            _serviceEmail = new ServiceEmailSecurity();
        }

        public DtoLoginResponse LoginDummy(DtoLogin userLogin)
        {
            DtoLoginResponse dtoLoginResponse = new DtoLoginResponse();
            dtoLoginResponse.StatusLogin = StatusLogin.Ok;
            return dtoLoginResponse;
        }


        public DtoLoginResponse Login(DtoLogin userLogin)
        {
            DtoLoginResponse dtoLoginResponse = new DtoLoginResponse();

            UserFkw user = (_serviceUser.GetUserByUserName(userLogin.UserName));
            string passDb = string.Empty;

            if (user is null)
            {
                dtoLoginResponse.StatusLogin = StatusLogin.UserOrPasswordIncorrect;
                return dtoLoginResponse;
            }

            passDb = _serviceCryptography.Descrypt(user.Password, user.Id.ToString());

            if (!passDb.Equals(userLogin.Password))
            {
                _serviceUser.SaveUserLoginAttempt(user.Id,
                        "PasswordIncorrect");
                dtoLoginResponse.StatusLogin = StatusLogin.UserOrPasswordIncorrect;
                return dtoLoginResponse;
            }

            _serviceUser.UpdateStatusBlocked(user.Id);

            if (user.UserBlocked)
            {
                dtoLoginResponse.StatusLogin = StatusLogin.UserBlocked;
                return dtoLoginResponse;
            }


            if (user.LoginSessions >= 1)
            {
                dtoLoginResponse.StatusLogin = StatusLogin.ExistSession;
                return dtoLoginResponse;
            }

            if (user.ApplyToken)
                _serviceToken.CreateToken(user);

            user.Role = _serviceRole.GetRole(user.Id);

            if (user.Role is null)
            {
                dtoLoginResponse.StatusLogin = StatusLogin.RoleNotAssigned;
                return dtoLoginResponse;
            }
            DtoUserFkw dtoUserFkw = new DtoUserFkw();
            DtoUserToken dtoUserToken = new DtoUserToken();
            DtoRole dtoRole = new DtoRole();
            DtoUserInformation dtoUserInformation = new DtoUserInformation();

            dtoUserFkw.Id = user.Id;
            dtoUserFkw.UserName = user.UserName;
            dtoUserFkw.Password = user.Password;
            dtoUserFkw.DateCreated = user.DateCreated;
            dtoUserFkw.UserCreated = user.UserCreated;
            dtoUserFkw.LoginSessions = user.LoginSessions;
            dtoUserFkw.UserBlocked = user.UserBlocked;

            //Toeken
            dtoUserToken.UserId = user.UserToken.UserId;
            dtoUserToken.Token = user.UserToken.Token;
            dtoUserToken.DateExpiration = user.UserToken.DateExpiration;
            dtoUserFkw.UserToken = dtoUserToken;
            dtoUserFkw.RolId = user.RolId;

            //Role
            //dtoUserFkw.Role.
            dtoLoginResponse.User = dtoUserFkw;
            _serviceUser.UpdateLoginSessions(user.Id);
            return dtoLoginResponse;

        }

        public void GenerateChangePasswordRequest(Guid userId,
            string url)
        {
            UserFkw userFkw = _serviceUser.GetUserById(userId);
            DateTime dateTime = DateTime.Now;
            ChangePasswordRequest changePasswordRequest =
                ChangePasswordRequest.Create(userId, dateTime.AddHours(2), dateTime);
            _changePasswordRequestRepo.Save(changePasswordRequest);
            _serviceEmail.SendEmailForgetPassword(userFkw.UserName, userFkw.UserInformation.Email, url);
        }

        public void ChangePassword(DtoChangePassword dtoChangePassword)
        {
            bool userExist = _serviceUser.UserExistByUserNameAndEmail(dtoChangePassword.UserName,dtoChangePassword.Email);
            UserFkw userFkw = null;
            string actualPassword = string.Empty;
            bool isUpdate = false;
            bool isValidRequest = false;

            ChangePasswordRequest changePasswordRequest = 
                _changePasswordRequestRepo.SelectByIdRequest(dtoChangePassword.IdRequest);

            isValidRequest = changePasswordRequest != null 
                && DateTime.Now < changePasswordRequest.DateExpired;

            if (!isValidRequest)
            {
                throw new Exception("The request of password was expired.");
            }

            if (userExist & isValidRequest)
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

                //Email alert of request
                _serviceEmail.EmailValidForgetPassword(userFkw.UserName,userFkw.UserInformation.Email);

            }


        }
    }
}
