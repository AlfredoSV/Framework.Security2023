namespace Framework.Security2023.Entities
{
    public enum StatusLogin
    {
        Ok = 1,
        UserOrPasswordIncorrect = -1,
        UserBlocked = -2,
        ExistSession = -3,
        TokenNotValid = -4

    }

    public class Login
    {
  
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private StatusLogin _statusLogin;
        public StatusLogin StatusLog
        {
            get { return _statusLogin; }
            set { _statusLogin = value; }
        }

        private UserFkw _user;
        public UserFkw User
        {
            get { return _user; }
            set { _user = value; }
        }

        private Login(string user, string password, StatusLogin status)
        {
            UserName = user;
            StatusLog = status;
            Password = password;
        }

        public static Login Create(string user, string password, StatusLogin status = StatusLogin.Ok)
        {
            return new Login(user, password, status);
        }
    }

}
