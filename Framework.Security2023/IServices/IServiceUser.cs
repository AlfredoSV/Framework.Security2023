using Framework.Security2023.Entities;
using System;
using System.Threading.Tasks;

namespace Framework.Security2023.IServices
{
    public interface IServiceUser
    {
        Task<bool> CreateUser(UserFkw newUser, bool isCreatedByAdmin);
        Task<UserFkw> GetUserByUserName(string userName);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> UpdatePassword(UserInformation userInformation);
        Task<bool> UpdateUser(UserFkw user);
        Task<bool> UserExist(string email = "DEFAULT", string userName = "DEFAULT");
        Task UpdateLoginSessions(Guid userId, int sessions);
        Task<UserFkw> GetUserById(Guid userId);
        Task UpdateStatusBlocked(Guid userId);
        void SaveUserLoginAttempt(Guid userId, string description);
    }
}
