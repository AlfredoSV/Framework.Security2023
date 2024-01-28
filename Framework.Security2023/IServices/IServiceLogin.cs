using Framework.Security2023.Dtos;
using System;

namespace Framework.Security2023.IServices
{
    public interface IServiceLogin
    {
        DtoLoginResponse Login(DtoLogin login);
        void SignOut(Guid userId);
        //DtoLoginResponse LoginDummy(DtoLogin userLogin);
        void ChangePassword(DtoChangePassword dtoChangePassword);
        void GenerateChangePasswordRequest(string userName,
            string urlBase);

    }
}
