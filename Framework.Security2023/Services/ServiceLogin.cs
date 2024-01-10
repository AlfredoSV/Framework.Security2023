using Framework.Security2023.Cryptography;
using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Linq;

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

        //public DtoLoginResponse LoginDummy(DtoLogin userLogin)
        //{
        //    DtoLoginResponse dtoLoginResponse = new DtoLoginResponse();
        //    dtoLoginResponse.StatusLogin = StatusLogin.Ok;
        //    return dtoLoginResponse;
        //}

        public DtoLoginResponse Login(DtoLogin userLogin)
        {
            string passRequest = string.Empty;
            DtoUserFkw dtoUserFkw = new DtoUserFkw();
            DtoUserToken dtoUserToken = new DtoUserToken();
            DtoRole dtoRole = new DtoRole();
            DtoUserInformation dtoUserInformation = new DtoUserInformation();
            DtoLoginResponse dtoLoginResponse = new DtoLoginResponse();
            UserFkw user = (_serviceUser.GetUserByUserName(userLogin.UserName));

            if (user == null)
            {
                dtoLoginResponse.StatusLogin = StatusLogin.UserOrPasswordIncorrect;
                return dtoLoginResponse;
            }

            passRequest = _serviceCryptography.Encrypt(userLogin.Password, user.Id.ToString());

            if (!passRequest.Equals(user.Password))
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


            dtoUserFkw.Id = user.Id;
            dtoUserFkw.UserName = user.UserName;
            dtoUserFkw.Password = user.Password;
            dtoUserFkw.DateCreated = user.DateCreated;
            dtoUserFkw.UserCreated = user.UserCreated;
            dtoUserFkw.LoginSessions = user.LoginSessions;
            dtoUserFkw.UserBlocked = user.UserBlocked;

            //Token
            dtoUserToken.UserId = user.UserToken.UserId;
            dtoUserToken.Token = user.UserToken.Token;
            dtoUserToken.DateExpiration = user.UserToken.DateExpiration;
            dtoUserFkw.UserToken = dtoUserToken;

            //Role
            dtoUserFkw.RolId = user.RolId;
            dtoUserFkw.Role = new DtoRole();
            dtoUserFkw.Role.Id = user.Role.Id;
            dtoUserFkw.Role.RolName = user.Role.RolName;
            dtoUserFkw.Role.DateCreated = user.Role.DateCreated;

            user.Role.Permissions.ForEach((permission) =>
            {
                dtoUserFkw.Role.Permissions.Add(new DtoPermission()
                {
                    Permision = permission.Permision,
                    Id = permission.Id,
                    Description = permission.Description,
                    Module = permission.Module,
                    RolId = permission.RolId
                });
            });

            dtoUserFkw.Role.UserCreated = user.Role.UserCreated;
            dtoUserFkw.Role.Status = user.Role.Status;

            //DtoUserInformation
            dtoUserFkw.UserInformation = new DtoUserInformation();
            dtoUserFkw.UserInformation.IdUser = user.UserInformation.IdUser;
            dtoUserFkw.UserInformation.Name = user.UserInformation.Name;
            dtoUserFkw.UserInformation.LastName = user.UserInformation.LastName;
            dtoUserFkw.UserInformation.Age = user.UserInformation.Age;
            dtoUserFkw.UserInformation.DateCreated = user.UserInformation.DateCreated;
            dtoUserFkw.UserInformation.Address = user.UserInformation.Address;
            dtoUserFkw.UserInformation.Email = user.UserInformation.Email;
            dtoUserFkw.UserInformation.UserCreated = user.UserInformation.UserCreated;

            dtoUserFkw.ApplyToken = user.ApplyToken;
            dtoLoginResponse.User = dtoUserFkw;
            _serviceUser.UpdateLoginSessions(user.Id);
            return dtoLoginResponse;

        }

        public void GenerateChangePasswordRequest(string userName,
            string urlBase)
        {
            UserFkw userFkw = _serviceUser.GetUserByUserName(userName);
            DateTime dateTime = DateTime.Now;
            ChangePasswordRequest changePasswordRequest =
                ChangePasswordRequest.Create(userFkw.Id, dateTime.AddHours(2), dateTime);

            if (string.IsNullOrEmpty(urlBase))
                throw new ArgumentNullException("urlBase");

            _changePasswordRequestRepo.Save(changePasswordRequest);
            urlBase += $"/{userFkw.Id}/{changePasswordRequest.IdRequest}";
            _serviceEmail.SendEmailForgetPassword(userFkw.UserName, userFkw.UserInformation.Email, urlBase);

        }

        public void ChangePassword(DtoChangePassword dtoChangePassword)
        {
            string actualPassword;
            bool isUpdate;
            bool isValidRequest;
            ChangePasswordRequest changePasswordRequest;
            UserFkw userFkw;

            bool userExist =
            _serviceUser.UserExistByUserName(dtoChangePassword.UserName) &&  _serviceUser.UserExistByEmail(dtoChangePassword.Email);

            if (!userExist)
                throw new Exception("The user was not exist.");

            changePasswordRequest =
                _changePasswordRequestRepo.SelectByIdRequest(dtoChangePassword.IdRequest);

            isValidRequest = changePasswordRequest != null
                && DateTime.Now < changePasswordRequest.DateExpired;

            if (!isValidRequest)
                throw new Exception("The request of password was expired.");

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
                _serviceEmail.EmailValidForgetPassword(userFkw.UserName, userFkw.UserInformation.Email);

            }


        }
    }
}
