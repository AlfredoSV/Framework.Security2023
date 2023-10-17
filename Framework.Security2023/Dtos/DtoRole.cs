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
        private Guid Id { get; set; }
        private IEnumerable<Permission> Permissions { get; set; }
        private string RolName;
        private DateTime DateCreated;
        private Guid UserCreated;
        private bool Status;
    }
}
