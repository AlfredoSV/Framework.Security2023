using System;

namespace Framework.Security2023.Dtos
{
    public class DtoUserToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime DateExpiration { get; set; }

    }
}
