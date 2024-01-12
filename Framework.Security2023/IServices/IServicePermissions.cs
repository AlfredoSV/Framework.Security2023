using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.IServices
{
    internal interface IServicePermissions
    {
        bool SavePermissions(IEnumerable<Permission> permission);
        IEnumerable<Permission> GetPermission(Guid idRole);
    }
}
