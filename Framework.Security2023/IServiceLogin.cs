using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public interface IServiceLogin
    {
        Login Login(Login login);

        bool CreateUser(User newUser);

        bool DeleteUser(User user);

        bool UpdatePassword(User user);

        bool UpdateUser(User user);
    }
}
