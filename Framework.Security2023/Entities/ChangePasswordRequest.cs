using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    class ChangePasswordRequest
    {
        public Guid IdRequest { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateExpired { get; set; }
        public DateTime DateCreated { get; set; }

        private ChangePasswordRequest(Guid idRequest, Guid userId, DateTime dateExpired, DateTime dateCreated)
        {
            IdRequest = idRequest;
            UserId = userId;
            DateExpired = dateExpired;
            DateCreated = dateCreated;
        }

        public static ChangePasswordRequest Create(Guid idRequest, Guid userId, DateTime dateExpired, DateTime dateCreated)
        {
            return new ChangePasswordRequest(idRequest, userId, dateExpired, dateCreated);
        }

    }
}
