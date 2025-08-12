using Framework.Security2023.Dtos;
using System;
using System.Threading.Tasks;

namespace Framework.Security2023.IServices
{
    public interface IServiceLogin
    {
        Task<DtoLoginResponse> Login(DtoLogin login);
        void SignOut(Guid userId);
        Task ChangePassword(DtoChangePassword dtoChangePassword);
        Task GenerateChangePasswordRequest(string userName,
            string urlBase);

    }
}
