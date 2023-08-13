using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
        public string NameRol { get; set; }
    }
}
