using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public class ServiceToken : IServiceToken
    {
        private readonly RepositoryToken _repositoryToken;

        public ServiceToken()
        {
            _repositoryToken = new RepositoryToken();
        }

        public UserToken CreateToken(UserFkw userFkw)
        {
            UserToken userToken = UserToken.Create(userFkw.Id);

            _repositoryToken.Save(userToken);

            return userToken;
        }

        public bool IsValidToken(Guid userId, string token)
        {
            UserToken userToken = _repositoryToken.GetLastToken(userId);

            if (!token.Equals(userToken.Token))           
                return false;
            if (DateTime.Now > userToken.DateExpiration)
                return false;

            return true;
        }
    }
}
