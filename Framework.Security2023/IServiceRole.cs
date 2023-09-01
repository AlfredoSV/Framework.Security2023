using Framework.Security2023.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public interface IServiceRole
    {
        bool RoleExist(Guid rolId);
        Role GetRole(Guid userId);
    }
}
