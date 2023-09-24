using System;

namespace Framework.Security2023.Entities
{
    public class UserToken
    {

        private Guid id;
        private Guid userId;
        private string token;
        private DateTime dateCreated;
        private DateTime dateExpiration;

        public Guid Id { get => id; set => id = value; }
        public Guid UserId { get => userId; set => userId = value; }
        public string Token { get => token; set => token = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public DateTime DateExpiration { get => dateExpiration; set => dateExpiration = value; }

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
