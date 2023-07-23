using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Entities
{
    public class UserFkw
    {
		//[Id]
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public DateTime DateCreated { get; set; }
		public Guid UserCreated { get; set; }
		public int LoginSessions { get; set; }
		public bool UserBlocked { get; set; }

	}
}
