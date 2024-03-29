﻿using Framework.Security2023.Entities;
using System;

namespace Framework.Security2023.IServices
{
    public interface IServiceUser
    {
        bool CreateUser(UserFkw newUser, bool isCreatedByAdmin);
        UserFkw GetUserByUserName(string userName);
        bool DeleteUser(Guid userId);
        bool UpdatePassword(Guid userId, string newPassword);
        bool UpdateUser(UserFkw user);
        bool UserExistByEmail(string email);
        bool UserExistByUserName(string userName);
        void UpdateLoginSessions(Guid userId, int sessions);
        UserFkw GetUserById(Guid userId);
        void UpdateStatusBlocked(Guid userId);
        void SaveUserLoginAttempt(Guid userId, string description);

    }
}
