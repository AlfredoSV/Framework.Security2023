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
	public class TestManagmentTokenUser
	{
		[TestMethod]
		public void CreateToken()
		{
			IServiceToken serviceToken = new ServiceToken();

			UserFkw userFkw = UserFkw.Create(Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC"),
				"Test1Up", "test1", DateTime.Now, Guid.Parse("4523F025-97F3-4BBF-A94B-CBCA3A0A4DD5"), 0, false, true,
				Guid.Parse("0CB2008F-A9ED-497A-9551-32AE3C431386"));
			
			UserToken userToken = serviceToken.CreateToken(userFkw);

			Assert.IsNotNull(userToken);
		
		}
	}
}
