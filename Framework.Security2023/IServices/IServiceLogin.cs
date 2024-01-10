using Framework.Security2023.Dtos;

namespace Framework.Security2023.IServices
{
    public interface IServiceLogin
    {
        DtoLoginResponse Login(DtoLogin login);
        //DtoLoginResponse LoginDummy(DtoLogin userLogin);
        void ChangePassword(DtoChangePassword dtoChangePassword);
        void GenerateChangePasswordRequest(string userName,
            string urlBase);

    }
}
