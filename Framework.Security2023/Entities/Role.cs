using System;
using System.Collections.Generic;

namespace Framework.Security2023.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
        public string NameRol { get; set; }

        private Role(Guid id, string nameRol)
        {
            Id = id;
            NameRol = nameRol;
        }

        public static Role Create(Guid id, string nameRol)
        {
            return new Role(id, nameRol);
        }
       
    }
}
