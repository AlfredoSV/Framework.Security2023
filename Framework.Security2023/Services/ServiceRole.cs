using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Framework.Security2023.Services
{
    public class ServiceRole : IServiceRole
    {
        private readonly RepositoryRole _repositoryRole;
        private readonly IServicePermissions _servicePermissions;

        public ServiceRole()
        {
            _servicePermissions = new ServicePermissions();
            _repositoryRole = new RepositoryRole(); 
        }

        public async Task<Role> GetRole(Guid userId)
        {
            IEnumerable<Permission> permissions = new List<Permission>();
            Role role = _repositoryRole.GetRoleByUserId(userId);

            if (role is null)
                throw new NullReferenceException(nameof(role));

             permissions = await _servicePermissions.GetPermission(role.Id);

            role.SetPermissions(permissions);
            return role;
        }

        public bool RoleExist(Guid roleId)
        {
            return _repositoryRole.GetRoleById(roleId) != null;
        }

        public async Task<bool> Create(Role role)
        {
            return _repositoryRole.InsertRole(role)  &&  await _servicePermissions.SavePermissions(role.Permissions) ;
        }
    }
}
