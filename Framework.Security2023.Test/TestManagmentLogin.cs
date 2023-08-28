using Framework.Security2023.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Test
{
	[TestClass]
	public class TestManagmentLogin
	{
		[TestMethod]
		public void ValidateLogin()
		{
			Login userLogin = Login.Create("Test1Up", "lalalal");
			IServiceLogin serviceLogin = new ServiceLogin();
			var response = serviceLogin.Login(userLogin);
			Assert.AreEqual(Login.StatusLogin.Ok, response.StatusLog);


		}
		[TestMethod]
		[Ignore]
		public void ValidateLoginDummy()
		{
			Login userLogin = Login.Create("lalla", "lalalal");
			IServiceLogin serviceLogin = new ServiceLogin();
			var response = serviceLogin.LoginDummy(userLogin);
			Assert.AreEqual(Login.StatusLogin.Ok, response.StatusLog);

		}
	}
}
