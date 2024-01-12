using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Permission> GetPermission(Guid idRole)
        {
            return _repositoryPermission.GetPermission(idRole);
        }

        public bool SavePermissions(IEnumerable<Permission> permission)
        {
            return _repositoryPermission.InsertPermissions(permission.ToList());
        }
    }
}
