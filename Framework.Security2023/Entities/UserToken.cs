using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class UserToken
    {
        
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateExpiration { get; private set; }

        private  UserToken( Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DateCreated = DateTime.Now;
            DateExpiration = DateCreated.AddDays(1);
            Token = $"{Id}-" +
                $"{DateCreated.Day}-{DateCreated.Hour}-" +
                $"{DateExpiration.Day}-{DateExpiration.Hour}";
            
        }

        public UserToken(Guid id, Guid userId, string token, DateTime dateCreated, DateTime dateExpiration)
        {
            UserId = userId;
            Token = token;
            DateCreated = dateCreated;
            DateExpiration = dateExpiration;
        }

        public static UserToken Create( Guid userId)
        {
            return new UserToken(userId);
        }

        public static UserToken Create(Guid id, Guid userId, string token, DateTime dateCreated, DateTime dateExpiration)
        {
            return new UserToken( id,  userId,  token,  dateCreated,  dateExpiration);
        }
    }
}
