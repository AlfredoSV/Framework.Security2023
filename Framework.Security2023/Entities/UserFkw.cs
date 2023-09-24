using System;

namespace Framework.Security2023.Entities
{
    public class UserFkw
    {
        private Guid _id;
        private string _userName;
        private string _password;
        private DateTime _dateCreated;
        private Guid _userCreated;
        private int _loginSessions;
        private bool _userBlocked;
        private UserToken _userToken;      
        private Guid _rolId;
        private Role _role;
        private bool _applyToken;
        private UserInformation _userInformation;


        public UserInformation UserInformation { get => _userInformation; 
            set {

                if (_id == Guid.Empty)
                    throw new ArgumentNullException("The user id is null, not valid.");

                value.IdUser = this.Id;
                _userInformation = value;

            }
        }
        public Guid RolId { get => _rolId; set => _rolId = value; }
        public Role Role { get => _role; set => _role = value; }
        public bool ApplyToken { get => _applyToken; set => _applyToken = value; }
        public Guid Id { get => _id; set => _id = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public DateTime DateCreated { get => _dateCreated; set => _dateCreated = value; }
        public Guid UserCreated { get => _userCreated; set => _userCreated = value; }
        public int LoginSessions { get => _loginSessions; set => _loginSessions = value; }
        public bool UserBlocked { get => _userBlocked; set => _userBlocked = value; }
        public UserToken UserToken { get => _userToken; set => _userToken = value; }
        public Guid RolId1 { get => _rolId; set => _rolId = value; }
        public Role Role1 { get => _role; set => _role = value; }
        public bool ApplyToken1 { get => _applyToken; set => _applyToken = value; }

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
