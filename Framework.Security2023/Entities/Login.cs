using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class Login
    {
        public enum StatusLogin
        {
            Ok = 1,
            UserOrPasswordIncorrect = -1,
            UserBlocked = -2,
            ExistSession = -3,
            TokenNotValid = -4

        }
        private string _User;
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private StatusLogin _StatusLogin;
        public StatusLogin StatusLog
        {
            get { return _StatusLogin; }
            set { _StatusLogin = value; }
        }

        private Role _role;

        public Role Role
        {
            get { return _role; }
            set { _role = value; }
        }


        private Login(string user, string password, StatusLogin status)
        {
            this.User = user;
            this.StatusLog = status;
            this.Password = password;
        }

        public static Login Create(string user, string password, StatusLogin status = StatusLogin.Ok)
        {
            return new Login(user, password, status);
        }
    }

}
