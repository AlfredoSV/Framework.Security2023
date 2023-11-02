using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Dtos
{
    public class DtoRole
    {
        public Guid Id { get; set; }
        public List<DtoPermission> Permissions { get; set; }
        public string RolName;
        public DateTime DateCreated;
        public Guid UserCreated;
        public bool Status;
    }
}
