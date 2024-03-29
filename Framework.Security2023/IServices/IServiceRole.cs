using Framework.Security2023.Entities;
using System;
using System.Threading.Tasks;

namespace Framework.Security2023.IServices
{
    public interface IServiceRole
    {
        bool RoleExist(Guid rolId);
        Task<Role> GetRole(Guid userId);
        Task<bool> Create(Role role);

    }
}
