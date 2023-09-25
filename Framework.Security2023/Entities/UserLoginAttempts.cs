using System;


namespace Framework.Security2023.Entities
{
    public class UserLoginAttempts
    {
        private Guid _idUser;
        private string _description;
        private DateTime _dateCreated;

        public Guid IdUser { get => _idUser; set => _idUser = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime DateCreated { get => _dateCreated; set => _dateCreated = value; }

        private UserLoginAttempts(Guid idUser, string description)
        {
            _idUser = idUser;
            _description = description;
            _dateCreated = DateTime.Now;
        }

        public static UserLoginAttempts Create(Guid idUser, string description)
        {
            return new UserLoginAttempts( idUser,  description);
        }

    }
}
