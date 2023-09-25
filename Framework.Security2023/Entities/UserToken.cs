using System;

namespace Framework.Security2023.Entities
{
    public class UserToken
    {

        private Guid _id;
        private Guid _userId;
        private string _token;
        private DateTime _dateCreated;
        private DateTime _dateExpiration;

        public Guid Id { get => _id; set => _id = value; }
        public Guid UserId { get => _userId; set => _userId = value; }
        public string Token { get => _token; set => _token = value; }
        public DateTime DateCreated { get => _dateCreated; set => _dateCreated = value; }
        public DateTime DateExpiration { get => _dateExpiration; set => _dateExpiration = value; }

        private  UserToken( Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DateCreated = DateTime.Now;
            DateExpiration = DateCreated.AddDays(1);
            Token = $"{Id}{DateCreated.Day}{DateCreated.Hour}{DateExpiration.Day}{DateExpiration.Hour}";         
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
