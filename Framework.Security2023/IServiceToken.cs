using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public interface IServiceToken
    {
        UserToken CreateToken(UserFkw userFkw);
        bool IsValidToken(Guid idUser, string token);

    }
}
