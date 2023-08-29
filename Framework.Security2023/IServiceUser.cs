using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
	public interface IServiceUser
	{
		bool CreateUser(UserFkw newUser, bool isCreatedByAdmin);
		bool DeleteUser(Guid userId);
		bool UpdatePassword(Guid userId, string newPassword);
		bool UpdateUser(UserFkw user);
		bool UserExist(string userName);
		Role GetRole(Guid userId);
		UserFkw GetUserById(Guid userId);
	}
}
