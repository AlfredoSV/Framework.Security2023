using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public class ServiceToken : IServiceToken
    {
        public UserToken CreateToken(UserFkw userFkw)
        {
            throw new NotImplementedException();
        }

        public bool IsValidToken(UserFkw userFkw, UserToken userToken)
        {
            throw new NotImplementedException();
        }
    }
}
