using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> CreateUser(UserFkw newUser, bool isCreatedByAdmin)
        {
            if (newUser is null)
                throw new ArgumentNullException( string.Format(Resources.ObjectIsNullMessage,nameof(newUser)));

            if (newUser.UserInformation is null)
                throw new ArgumentNullException(string.Format(Resources.ObjectIsNullMessage,nameof(newUser.UserInformation)));

            if (!_serviceRole.RoleExist(newUser.RolId))
                throw new ApplicationException(string.Format(Resources.ValueNotExistsMessage,"Role"));


            newUser.Password = isCreatedByAdmin ? _serviceCryptography.Encrypt(newUser.UserName, newUser.Id.ToString())
                                                : _serviceCryptography.Encrypt(newUser.Password, newUser.Id.ToString());

            return await _respositoryUser.Save(newUser);
        }

        public async Task<bool> DeleteUser(Guid userId) => await _respositoryUser.Delete(userId);

        public async Task<bool> UpdateUser(UserFkw user)
        {
            int result = await _respositoryUser.Update(user);
            return result > 0;
        }

        async Task<UserFkw> IServiceUser.GetUserById(Guid userId) => await _respositoryUser.GetUser(userId);

        async Task<UserFkw> IServiceUser.GetUserByUserName(string userName)
        {
            return (await _respositoryUser.GetUserByUserName(userName)).Data;
        }

        async Task<bool> IServiceUser.UpdatePassword(UserInformation userInformation)
        {
            return await _respositoryUser.UpdatePassword(userInformation);
        }

        async Task IServiceUser.UpdateStatusBlocked(Guid userId)
        {
            IEnumerable<UserLoginAttempts> userLoginAttempts = _repositoryUserLoginAttempts.GetLoginAttemptsByUserId(userId);

            if (userLoginAttempts.Count() >= 3)
                await _respositoryUser.UpdateStatusBlocked(userId, true);
        }

        void IServiceUser.SaveUserLoginAttempt(Guid userId, string description)
        {
            _repositoryUserLoginAttempts.SaveLoginAttempt(UserLoginAttempts.Create(userId, description));
        }

        async Task IServiceUser.UpdateLoginSessions(Guid userId, int sessions)
        {
            await _respositoryUser.UpdateLoginSession(userId,sessions);
        }

        public async Task<bool> UserExist(string email = "DEFAULT", string userName = "DEFAULT")
        {
            bool existAnUser = (await _respositoryUser.ValidateUser(email)).Data;
            //bool existByUserName = (await _respositoryUser.ValidateUserByUserName(userName)).Data;

            return existAnUser;
        }

    }
}
