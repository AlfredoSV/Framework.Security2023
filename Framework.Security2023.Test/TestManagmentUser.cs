using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Security2023.Test
{

    [TestClass]
    public class TestManagmentUser
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceRole _serviceRole;

        public TestManagmentUser()
        {
            SlqConnectionStr.Instance.SqlConnectionString = "Server=Alfredo; Database=Framework_Users;User=sa;Password=1007";
            _serviceUser = new ServiceUser();
            _serviceRole = new ServiceRole();

        }

        [Ignore]
        [TestMethod]
        public void CreateRole()
        {
            IEnumerable<Permission> permissions = new List<Permission>();

            Guid idRole = Guid.Parse("35AE4DB6-0243-4B44-9B8B-C4E49ABD17E3");
            string roleName = "Admin";
            Guid userCreated = Guid.Parse("8EF38297-6B83-4C7A-A410-1A7E04F4D252");
            Role role = Role.Create(idRole, roleName, DateTime.Now, userCreated, true);


            Permission permissionOne = Permission.Create("GetUsers", Guid.Parse("E638B28C-E601-44B3-A853-753C2504BD35"), idRole, "Permiso para obtener usuarios", "Users", DateTime.Now, userCreated, true);
            Permission permissionTwo = Permission.Create("DeleteUsers", Guid.Parse("12086117-E316-4342-899B-164FCB03A69E"), idRole, "Permiso para borrar usuarios", "Users", DateTime.Now, userCreated, true);


            permissions = permissions.Append(permissionOne);
            permissions = permissions.Append(permissionTwo);
            role.SetPermissions(permissions);
            _serviceRole.Create(role);
        }

        [Ignore]
        [TestMethod]
        public void CreateUser()
        {
            Guid idRole = Guid.Parse("35AE4DB6-0243-4B44-9B8B-C4E49ABD17E3");
            Guid userCreated = Guid.Parse("8EF38297-6B83-4C7A-A410-1A7E04F4D252");

            UserFkw userFkw = UserFkw.Create("alfredo1007", "1007", userCreated, false, idRole);
            userFkw.UserInformation = UserInformation.Create("Alfredo", "Sanchez", 26, "NA", "alfredofake.com.mx", userCreated);
            _serviceUser.CreateUser(userFkw, true);
        }

        [TestMethod]
        public void GetUser()
        {
            Guid idUser = Guid.Parse("FACC6ADB-8A55-4EE0-88EE-2C341B112057");
            UserFkw userFkw = _serviceUser.GetUserByUserName("alfredo1007");
           
            Assert.IsNotNull(userFkw);
            
        }


    }
}
