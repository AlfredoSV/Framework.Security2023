﻿using Framework.Security2023.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class UserFkw
    {
       
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public DateTime DateCreated { get; private set; }
        public Guid UserCreated { get; private set; }
        public int LoginSessions { get; private set; }
        public bool UserBlocked { get; private set; }
        public UserToken UserToken { get; private set; }

        private UserFkw(Guid id, string userName, string password, DateTime dateCreated, Guid userCreated, int loginSessions, bool userBlocked)
        {
            Id = id;
            UserName = userName;
            Password = password;
            DateCreated = dateCreated;
            UserCreated = userCreated;
            LoginSessions = loginSessions;
            UserBlocked = userBlocked;
        }

        public static UserFkw Create(Guid id, string userName, string password, DateTime dateCreated, Guid userCreated, int loginSessions, bool userBlocked)
        {
            return new UserFkw( id,  userName,  password,  dateCreated,  userCreated,  loginSessions,  userBlocked);
        }

        public void SetToken(UserToken userToken)
        {
            UserToken = userToken;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }
    }
}
