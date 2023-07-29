using Framework.Security2023.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class UserFkw
    {
        //[Id]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserCreated { get; set; }
        public int LoginSessions { get; set; }
        public bool UserBlocked { get; set; }

        public UserFkw() { }

        public UserFkw(string userName, string password, Guid userCreated)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = (new ServiceCryptography()).Encrypt(password, Id.ToString());
            DateCreated = DateTime.Now;
            UserCreated = userCreated;
            LoginSessions = 0;
            UserBlocked = false;
        }

        public static UserFkw Create(string userName, string password, Guid userCreated)
        {
            return new UserFkw(userName, password, userCreated);
        }
    }
}
