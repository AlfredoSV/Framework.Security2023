using Framework.Security2023.Entities;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public class ServiceRole : IServiceRole
    {
        private readonly RepositoryRole _repositoryRole;
        private readonly RepositoryPermission _repositoryPermission;

        public ServiceRole()
        {
            _repositoryPermission = new RepositoryPermission();
            _repositoryRole = new RepositoryRole(); 
        }

        public Role GetRole(Guid userId)
        {
            Role role = _repositoryRole.GetRoleByUserId(userId);

            if (role is null)
                return null;

            IEnumerable<Permission> permissions = _repositoryPermission.GetPermission(role.Id);

            role.Permissions = permissions;

            return role;
        }

        public bool RoleExist(Guid roleId)
        {
            return _repositoryRole.GetRoleById(roleId) != null;
        }
    }
}
