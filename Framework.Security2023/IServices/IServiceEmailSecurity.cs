using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.IServices
{
    interface IServiceEmailSecurity
    {
        void SendEmailForgetPassword(string userName,
            string emailTo, Guid userId);

    }
}
