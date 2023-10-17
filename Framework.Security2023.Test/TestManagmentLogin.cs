using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Security2023.Test
{
	[TestClass]
	public class TestManagmentLogin
	{

		//public TestManagmentLogin()
		//{
		//	SlqConnectionStr.Instance.SqlConnectionString = "Server=Alfredo; Database=Framework_Users;User=sa;Password=1007";
		//}

		//[TestMethod]
		//[Ignore]
		//public void ValidateLoginPassIncorrect()
		//{
		//	IServiceLogin serviceLogin = new ServiceLogin();
		//	Login userLoginIncorect = Login.Create("Test1Up", "lalalssssal");
		//	Login responsefailed = serviceLogin.Login(userLoginIncorect);
		//	Assert.AreEqual(StatusLogin.UserOrPasswordIncorrect, responsefailed.StatusLog);
		//}
		
		//[TestMethod]
		//[Ignore]
		//public void ValidateLogin()
		//{
		//	Login userLogin = Login.Create("Test1Up", "test1");
		//	IServiceLogin serviceLogin = new ServiceLogin();
		//	Login response = serviceLogin.Login(userLogin);
		//	Assert.AreEqual(StatusLogin.Ok, response.StatusLog);
		//}


		//[TestMethod]
		//[Ignore]
		//public void ValidateLoginDummy()
		//{
		//	Login userLogin = Login.Create("lalla", "lalalal");
		//	IServiceLogin serviceLogin = new ServiceLogin();
		//	var response = serviceLogin.LoginDummy(userLogin);
		//	Assert.AreEqual(StatusLogin.Ok, response.StatusLog);
		//}

		//[TestMethod]
		//public void ValidateSessionExist()
		//{
		//	Login userLogin = Login.Create("Test1Up", "test1");
		//	IServiceLogin serviceLogin = new ServiceLogin();
		//	Login response = serviceLogin.Login(userLogin);
		//	Assert.AreEqual(StatusLogin.ExistSession, response.StatusLog);
		//}

	}
}
