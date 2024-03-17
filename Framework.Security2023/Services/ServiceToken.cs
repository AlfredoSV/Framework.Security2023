using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;

namespace Framework.Security2023.Services
{
    public class ServiceToken : IServiceToken
    {
        private readonly RepositoryToken _repositoryToken;

        public ServiceToken()
        {
            _repositoryToken = new RepositoryToken();
        }

        UserToken IServiceToken.CreateToken(UserFkw userFkw)
        {
            UserToken userToken = UserToken.Create(userFkw.Id);

            _repositoryToken.Save(userToken);         

            return userToken;
        }

        public bool IsValidToken(Guid userId, string token)
        {
            UserToken userToken = _repositoryToken.GetLastToken(userId);

            return token.Equals(userToken.Token) &&
                !(DateTime.Now > userToken.DateExpiration);               
        
        }

    
    }
}
