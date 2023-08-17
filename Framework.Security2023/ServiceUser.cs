﻿using Framework.Security2023.Cryptography;
using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public class ServiceUser
    {
        private readonly ServiceCryptography _serviceCryptography;
        private readonly RepositoryRole _repositoryRole;
        private readonly RespositoryUser _respositoryUser;
 
        public ServiceUser()
        {
            _serviceCryptography = new ServiceCryptography();
            _repositoryRole = new RepositoryRole();
            _respositoryUser = new RespositoryUser();
        }
        public bool CreateUser(UserFkw newUser, bool isCreatedByAdmin)
        {
            if (isCreatedByAdmin)
                newUser.SetPassword(_serviceCryptography.Encrypt(newUser.UserName,
                    newUser.Id.ToString()));

            _respositoryUser.Save(newUser);
            return true;
        }

        public bool DeleteUser(Guid userId)
        {
            int rowDelete = _respositoryUser.Delete(userId);

            return rowDelete > 0;
        }

        public bool UpdatePassword(Guid userId, string newPassword)
        {
            newPassword = _serviceCryptography.Encrypt(newPassword,
                userId.ToString());

            int rowUpdated = _respositoryUser.UpdatePassword(userId,
                newPassword);

            return (rowUpdated >= 1);
        }

        public bool UpdateUser(UserFkw user)
        {
            user.SetPassword(_serviceCryptography.Encrypt(user.Password, user.Id.ToString()));

            int res = _respositoryUser.Update(user);

            return res > 0;
        }

        public bool UserExist(string userName)
        {
            UserFkw user = (_respositoryUser.GetUser(userName));
            return user != null;
        }

        public Role GetRole(Guid userId)
        {
            Role role = _repositoryRole.GetRol(userId);

            return role;
        }
    }
}