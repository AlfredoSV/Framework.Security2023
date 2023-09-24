using Framework.Security2023.Entities;
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
		bool UserExist(string userName);
		UserFkw GetUserById(Guid userId);
	}
}
