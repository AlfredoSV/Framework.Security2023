using Framework.Security2023.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Framework.Security2023.Test
{
    [TestClass]
    public class TestManagmentUser
    {
        [TestMethod]
        [Ignore]
        public void TestCreateUser()
        {
            IServiceLogin serviceLogin = new ServiceLogin("");

            UserFkw user = new UserFkw() { Id = Guid.Parse("CAD8A946-D085-4BC0-9653-759A56088988"),
                DateCreated = DateTime.Now,
                LoginSessions = 0,
                Password = "", UserBlocked = false,
                UserCreated = Guid.Parse("3ACD51A8-9BF4-42B9-AE7B-CA5A308E580E"),
                UserName = "AlfredoSV0" };

            Login login = Login.Create("AlfredoSV0","AlfredoSV0"); 
            
            bool result = serviceLogin.CreateUser(user,true);

            Assert.IsTrue(result);

            Login loginRes = serviceLogin.Login(login);

            Assert.IsNotNull(loginRes);
        }
    }
}
