using Framework.Security2023.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security2023.Services
{
    class ServiceEmailSecurity : IServiceEmailSecurity
    {

        private string GenerateBodyPassword(string url, string userName)
        {


            return string.Empty;

        }

        private string GenerateUrlForgetPassword(Guid userId)
        {


            return string.Empty;

        }

        void IServiceEmailSecurity.SendEmailForgetPassword(string userName ,string email, string emailTo,Guid userId)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(email);
              
                message.To.Add(emailTo);
                string url = GenerateUrlForgetPassword(userId);
                message.Body = GenerateBodyPassword(url, userName);

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = SmtpConfigurationSecurityFkw.Instance.Port;
                    smtpClient.EnableSsl = SmtpConfigurationSecurityFkw.Instance.EnableSsl;
                    NetworkCredential networkCredential = new NetworkCredential();
                    networkCredential.UserName = SmtpConfigurationSecurityFkw.Instance.UserName;
                    networkCredential.Password = SmtpConfigurationSecurityFkw.Instance.Password;
                    smtpClient.Credentials = networkCredential;
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex) { }


        }

 
    }
}
