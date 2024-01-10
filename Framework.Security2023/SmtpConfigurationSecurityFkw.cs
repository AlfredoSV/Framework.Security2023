using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023
{
    public class SmtpConfigurationSecurityFkw
    {
        private static SmtpConfigurationSecurityFkw _instance;

        public static SmtpConfigurationSecurityFkw Instance
        {
            get
            {

                if (_instance is null)
                    _instance = new SmtpConfigurationSecurityFkw();
                return _instance;

            }
            private set { }
        }

        public Guid idTemplateEmailValidForgetPassword { get; set; }
        public Guid idTemplateSendEmailForgetPassword { get; set; }
        public int Port { get; private set; }

        public bool EnableSsl { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Host { get; set; }

        public void SetConfigurationSecurity(int port, bool
            enableSsl, string userName, string password, string host)
        {
            this.Port = port;
            this.EnableSsl = enableSsl;
            this.UserName = userName;
            this.Password = password;
            this.Host = host;

        }

        public void SetConfigurationSecurityEmail(Guid idTemplateEmailValidForgetPassword,
            Guid idTemplateSendEmailForgetPassword)
        {
            this.idTemplateSendEmailForgetPassword = idTemplateSendEmailForgetPassword;
            this.idTemplateEmailValidForgetPassword = idTemplateEmailValidForgetPassword;
        }

    }
}
