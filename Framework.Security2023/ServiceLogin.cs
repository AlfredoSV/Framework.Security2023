using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Framework.Security2023.Entities.Login;

namespace Framework.Security2023
{
    public class ServiceLogin : IServiceLogin
    {
        public Login Login(Login userLogin)
        {
            User user = (new RespositorieUser().GetUser(userLogin.User));

            if(user == null)
            {
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;
                return userLogin;
            }

            if (user.Password.Trim().Equals(userLogin.Password))
                userLogin.StatusLog = StatusLogin.UserOrPasswordIncorrect;

            if (user.UserBlocked)
                userLogin.StatusLog = StatusLogin.UserBlocked;

            if (user.LoginSessions >=1)
                userLogin.StatusLog = StatusLogin.ExistSession;

            return userLogin;
        }

        public bool CreateUser(User newUser,bool isCreatedByAdmin)
        {
            if (isCreatedByAdmin)
                newUser.Password = newUser.UserName;

            int rowsRegister = (new RespositorieUser().Save(newUser));

            return rowsRegister > 0;
        }

        public bool DeleteUser(Guid userId)
        {
            int rowsRegister = (new RespositorieUser().Delete(userId));

            return rowsRegister > 0;
        }

        public bool UpdatePassword(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
