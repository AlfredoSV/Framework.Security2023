using System;

namespace Framework.Security2023.Dtos
{
    public class DtoUserFkw
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserCreated { get; set; }
        public int LoginSessions { get; set; }
        public bool UserBlocked { get; set; }
        public Guid RolId { get; set; }    
        public bool ApplyToken { get; set; }
        public DtoUserToken UserToken { get; set; }
        public DtoUserInformation UserInformation { get; set; }
        public DtoRole Role { get; set; }

    }
}
