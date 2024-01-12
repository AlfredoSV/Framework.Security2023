using System;
using System.Collections.Generic;

namespace Framework.Security2023.Entities
{
    public class Role
    {
        private Guid _id;
        private IEnumerable<Permission> _permissions;
        private string _rolName;
        private DateTime _dateCreated;
        private Guid _userCreated;
        private bool _active;

        public Guid Id { get => _id; set => _id = value; }
        public IEnumerable<Permission> Permissions { get => _permissions; }
        public string RolName { get => _rolName; set => _rolName = value; }
        public DateTime DateCreated { get => _dateCreated; set => _dateCreated = value; }
        public Guid UserCreated { get => _userCreated; set => _userCreated = value; }
        public bool Active { get => _active; set => _active = value; }


        private Role(Guid idP, string nameRolP, DateTime dateCreatedP, Guid userCreatedP, bool statusP)
        {
            Id = idP;
            RolName = nameRolP;
            DateCreated = dateCreatedP;
            UserCreated = userCreatedP;
            Active = statusP;
        }

        public static Role Create(Guid id, string rolNAame, DateTime dateCreated, Guid userCreated, bool status)
        {
            return new Role(id, rolNAame,  dateCreated,  userCreated,  status);
        }

        public void SetPermissions(IEnumerable<Permission> permissions)
        {
            _permissions = permissions;
        }
       
    }
}
