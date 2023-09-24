using System;

namespace Framework.Security2023.Entities
{
    public class UserFkw
    {
        private Guid id;
        private string userName;
        private string password;
        private DateTime dateCreated;
        private Guid userCreated;
        private int loginSessions;
        private bool userBlocked;
        private UserToken userToken;
        private UserInformation userInformation;
        private Guid rolId;
        private Role role;
        private bool applyToken;

        public Guid Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public Guid UserCreated { get => userCreated; set => userCreated = value; }
        public int LoginSessions { get => loginSessions; set => loginSessions = value; }
        public bool UserBlocked { get => userBlocked; set => userBlocked = value; }
        public UserToken UserToken { get => userToken; set => userToken = value; }
        public UserInformation UserInformation { get => userInformation; 
            set {

                if (this.id == Guid.Empty)
                    throw new ArgumentNullException("The user id is null, not valid.");

                value.IdUser = this.Id;
                userInformation = value;

            }
        }
        public Guid RolId { get => rolId; set => rolId = value; }
        public Role Role { get => role; set => role = value; }
        public bool ApplyToken { get => applyToken; set => applyToken = value; }

        private UserFkw(Guid id, string userName, string password, 
            DateTime dateCreated, Guid userCreated, int loginSessions,
            bool userBlocked, bool applyToken, Guid rolId)
        {
            ApplyToken = applyToken;
            RolId = rolId;
            Id = id;
            UserName = userName;
            Password = password;
            DateCreated = dateCreated;
            UserCreated = userCreated;
            LoginSessions = loginSessions;
            UserBlocked = userBlocked;
        }

        private UserFkw(string userName, string password,
            Guid userCreated, 
             bool applyToken, Guid rolId)
        {
        
            ApplyToken = applyToken;
            RolId = rolId;
            Id = Guid.NewGuid();
            UserName = userName;
            Password = password;
            DateCreated = DateTime.Now;
            UserCreated = userCreated;
            LoginSessions = 0;
            UserBlocked = true;
        }

        internal static UserFkw Create(Guid id, string userName,
            string password, DateTime dateCreated, Guid userCreated,
            int loginSessions, bool userBlocked, bool applyToken,
            Guid rolId)
        {
            return new UserFkw( id,  userName,  password,  dateCreated,  userCreated,  loginSessions,  userBlocked,applyToken,rolId);
        }

        public static UserFkw Create( string userName,
                                      string password, Guid userCreated, bool applyToken,Guid rolId)
        {
            if (string.IsNullOrEmpty(userName) || 
                string.IsNullOrEmpty(password) || 
                userCreated == Guid.Empty ||
                rolId == Guid.Empty)
                throw new ArgumentNullException("You cannot initialize the object with \"null\" or \"empty\" values.");

            return new UserFkw( userName, password,  userCreated, applyToken, rolId);
        }

       
    }
}
