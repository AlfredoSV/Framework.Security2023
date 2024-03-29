using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Security2023.Services
{
    internal class ServicePermissions : IServicePermissions
    {
        private readonly RepositoryPermission _repositoryPermission;

        public ServicePermissions()
        {
            _repositoryPermission = new RepositoryPermission();
        }

        public async Task<IEnumerable<Permission>> GetPermission(Guid idRole)
        {
            return await _repositoryPermission.GetPermission(idRole);
        }

        public async Task<bool> SavePermissions(IEnumerable<Permission> permission)
        {
            return await _repositoryPermission.InsertPermissions(permission);
        }
    }
}
