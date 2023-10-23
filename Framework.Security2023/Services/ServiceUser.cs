using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Security2023.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly ServiceCryptography _serviceCryptography;
        private readonly RespositoryUser _respositoryUser;
        private readonly RepositoryUserLoginAttempts _repositoryUserLoginAttempts;
        private readonly IServiceRole _serviceRole;

        public ServiceUser()
        {
            _serviceRole = new ServiceRole();
            _serviceCryptography = new ServiceCryptography();
            _respositoryUser = new RespositoryUser();
            _repositoryUserLoginAttempts = new RepositoryUserLoginAttempts();
        }

        public bool CreateUser(UserFkw newUser, bool isCreatedByAdmin)
        {
            if (newUser is null)
                throw new ArgumentNullException("The object newUser is null.");

            if (newUser.UserInformation is null)
                throw new ArgumentNullException("The object UserInformation is null.");

            if (!_serviceRole.RoleExist(newUser.RolId))
                throw new ApplicationException("The role was not exist");

            if (isCreatedByAdmin)
                newUser.Password = _serviceCryptography.Encrypt(newUser.UserName,
                    newUser.Id.ToString());
            else
                newUser.Password = _serviceCryptography.Encrypt(newUser.Password,
                    newUser.Id.ToString());

            return _respositoryUser.Save(newUser);
        }

        public bool DeleteUser(Guid userId) => _respositoryUser.Delete(userId);

      
        public bool UpdateUser(UserFkw user)
        {
            user.Password = _serviceCryptography.Encrypt(user.Password, user.Id.ToString());

            int res = _respositoryUser.Update(user);

            return res > 0;
        }


        UserFkw IServiceUser.GetUserById(Guid userId) => _respositoryUser.GetUser(userId);

        UserFkw IServiceUser.GetUserByUserName(string userName)
        {
            return _respositoryUser.GetUserByUserName(userName);
        }

        bool IServiceUser.UpdatePassword(Guid userId,string newPassword)
        {
            newPassword = _serviceCryptography.Encrypt(newPassword,
                userId.ToString());

            int rowUpdated = _respositoryUser.UpdatePassword(userId,
                newPassword);

            return (rowUpdated >= 1);
        }
        
        void IServiceUser.UpdateStatusBlocked(Guid userId)
        {
            IEnumerable<UserLoginAttempts> userLoginAttempts = _repositoryUserLoginAttempts.GetLoginAttemptsByUserId(userId);

            if (userLoginAttempts.Count() >= 3)
                _respositoryUser.UpdateStatusBlocked(userId, true);

        }

        void IServiceUser.SaveUserLoginAttempt(Guid userId, string description)
        {
            _repositoryUserLoginAttempts.
                SaveLoginAttempt(UserLoginAttempts.Create(userId, description));
        }

        void IServiceUser.UpdateLoginSessions(Guid userId)
        {
            _respositoryUser.UpdateLoginSession(userId);
        }

        public bool UserExistByUserNameAndEmail(string userName, string email) => (_respositoryUser.ValidateUser(userName, email));

    }
}
