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
        public UserInformation UserInformation { get; private set; }
        public Guid RolId { get; set; }
        public Role Role { get; private set; }
        public bool ApplyToken { get; set; }

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
            string password, Guid userCreated, bool applyToken,
            Guid rolId)
        {
            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password) && userCreated == Guid.Empty &&
            rolId == Guid.Empty)
                throw new ArgumentNullException("You cannot initialize the object with \"null\" or \"empty\" values.");

            return new UserFkw( userName, password,  userCreated, applyToken, rolId);
        }

        public void SetToken(UserToken userToken)
        {
            UserToken = userToken;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetRole(Role role)
        {
            Role = role;
        }

        public void SetUserInformation(UserInformation userInformation)
        {
            if(this.Id == Guid.Empty)
                    throw new ArgumentNullException("The user id is null, not valid.");

            userInformation.SetIdUser(this.Id);
            UserInformation = userInformation;
        }
    }
}
