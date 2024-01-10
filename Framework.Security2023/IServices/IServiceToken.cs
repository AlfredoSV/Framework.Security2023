using Framework.Security2023.Entities;
using System;

namespace Framework.Security2023.IServices
{
    public interface IServiceToken
    {
        UserToken CreateToken(UserFkw userFkw);
        bool IsValidToken(Guid idUser, string token);

    }
}
