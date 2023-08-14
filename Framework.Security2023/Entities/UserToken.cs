using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class UserToken
    {
        
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpiration { get; set; }

        private  UserToken(Guid id, Guid userId, string token, DateTime dateCreated, DateTime dateExpiration)
        {
            Id = id;
            UserId = userId;
            Token = token;
            DateCreated = dateCreated;
            DateExpiration = dateExpiration;
        }

        public static UserToken Create(Guid id, Guid userId, string token, DateTime dateCreated, DateTime dateExpiration)
        {
            return new UserToken( id,  userId,  token,  dateCreated,  dateExpiration);
        }
    }
}
