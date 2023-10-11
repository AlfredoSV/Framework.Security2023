using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Framework.Security2023.Test
{

    [TestClass]
    public class TestManagmentUser
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceRole _serviceRole;

        public TestManagmentUser()
        {
            _serviceUser = new ServiceUser();
            _serviceRole = new ServiceRole();
        }


        [TestMethod]
        [Ignore]
        public void GetUserById()
        {
            UserFkw user = _serviceUser.GetUserById(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"));

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.UserInformation);
        }

        [TestMethod]
        [Ignore]
        public void GetRoleWhenNoExist()
        {
            
            Assert.IsNull(_serviceRole.GetRole(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC")));
        }
     
        [TestMethod]
        [Ignore]
        public void DeleteUser()
        {
           
            bool userDeleted = _serviceUser.DeleteUser(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"));

            Assert.IsTrue(userDeleted);

        }


    }
}
