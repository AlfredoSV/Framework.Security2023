using Framework.Security2023.Entities;
using System;

namespace Framework.Security2023
{
    public interface IServiceRole
    {
        bool RoleExist(Guid rolId);
        Role GetRole(Guid userId);
    }
}
