using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class User
    {
		public Guid Id;
		public string UserName;
		public string Password;
		public DateTime DateCreated;
		public Guid UserCreated;
		public int LoginSessions;
		public bool UserBlocked;

	}
}
