using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using System;

namespace Framework.Security2023.IServices
{
    public interface IServiceLogin
    {
        DtoLoginResponse Login(DtoLogin login);
        DtoLoginResponse LoginDummy(DtoLogin userLogin);
        void ChangePassword(DtoChangePassword dtoChangePassword);
        void GenerateChangePasswordRequest(Guid userId);

    }
}
