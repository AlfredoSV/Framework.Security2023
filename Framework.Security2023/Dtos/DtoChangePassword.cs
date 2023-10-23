using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Dtos
{
    public class DtoChangePassword
    {
        public Guid IdRequest { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
