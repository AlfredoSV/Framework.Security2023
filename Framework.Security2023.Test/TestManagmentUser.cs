//using Framework.Security2023.Entities;
//using Framework.Security2023.IServices;
//using Framework.Security2023.Services;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace Framework.Security2023.Test
//{
//	[TestClass]
//	public class TestManagmentUser
//	{

//		[TestMethod]
//		[Ignore]
//		public void GetUserById()
//		{
//			ServiceUser serviceUser = new ServiceUser();

//			UserFkw user = serviceUser.GetUserById(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"));

//			Assert.IsNotNull(user);
//			Assert.IsNotNull(user.UserInformation);
//		}

//		[TestMethod]
//		[Ignore]
//		public void GetRoleWhenNoExist()
//		{
//			IServiceRole serviceRole = new ServiceRole();

//			Assert.IsNull(serviceRole.GetRole(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC")));
//		}

//		[TestMethod]
//		[Ignore]
//		public void UpdateUser()
//		{

//			//Check
//			//ServiceUser serviceUser = new ServiceUser();
//			//bool wasUserUpdate = false;
//   //         if (serviceUser.UserExist("Test1"))
//   //         {
//   //             UserFkw userFkw = UserFkw.Create(
//   //             "Test1Up", "test1", Guid.Parse("4523F025-97F3-4BBF-A94B-CBCA3A0A4DD5"), false,
//   //             Guid.Parse("0CB2008F-A9ED-497A-9551-32AE3C431386"));

//   //             wasUserUpdate = serviceUser.UpdateUser(userFkw);
//   //         }

//   //         Assert.IsTrue(wasUserUpdate);

//		}

//		[TestMethod]
//		[Ignore]
//		public void UpdatePassword()
//		{

//			ServiceUser serviceUser = new ServiceUser();

//			bool wasChange = serviceUser.
//				UpdatePassword(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"), "LALALAL");

//			Assert.IsTrue(wasChange);

//		}

//		[TestMethod]
//		[Ignore]
//		public void CreateUser()
//		{
//			ServiceUser serviceUser = new ServiceUser();


//			if (!serviceUser.UserExist("Test1"))
//			{
//				UserFkw userFkw = UserFkw.Create(
//				"Test1Up", "test1", Guid.Parse("4523F025-97F3-4BBF-A94B-CBCA3A0A4DD5"), false,
//				Guid.Parse("0CB2008F-A9ED-497A-9551-32AE3C431386"));

//				serviceUser.CreateUser(userFkw, true);
//			}

//			bool userExist = serviceUser.UserExist("Test1");

//			Assert.IsTrue(userExist);

//		}

//		[TestMethod]
//		[Ignore]
//		public void DeleteUser() {

//			ServiceUser serviceUser = new ServiceUser();

//			bool userDeleted = serviceUser.DeleteUser(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"));

//			Assert.IsTrue(userDeleted);

//		}

	
//	}
//}
