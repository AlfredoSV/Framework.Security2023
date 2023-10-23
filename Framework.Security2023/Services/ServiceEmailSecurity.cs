using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Framework.Security2023.Services
{
    class ServiceEmailSecurity : IServiceEmailSecurity
    {
        private readonly RepositoryTemplatesEmail _repositoryTemplatesEmail;
        
        public ServiceEmailSecurity()
        {
            _repositoryTemplatesEmail = new RepositoryTemplatesEmail();
        }

        public bool EmailValidForgetPassword(Guid userId, Guid idRequest)
        {
            throw new NotImplementedException();
        }

        private string GenerateBodyPassword(string url, string userName)
        {
            Dictionary<string, string> paramsBody = new Dictionary<string, string>
            {
                { "@userName", userName },
                { "@url", url }
            };
            Guid idTemplate = Guid.Parse("4479E1C7-E459-44CB-BB9E-93C158454CC2");
            TemplateEmail template = _repositoryTemplatesEmail.GetByid(idTemplate);

            foreach (KeyValuePair<string, string> paraBo in paramsBody)
            {
                template.BodyTemplate = template.BodyTemplate.Replace(paraBo.Key, paraBo.Value);
            }

            return template.BodyTemplate;
        }

        void IServiceEmailSecurity.SendEmailForgetPassword(string userName,
            string emailTo,string url,Guid userId)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(SmtpConfigurationSecurityFkw.Instance.UserName);
              
                message.To.Add(emailTo);
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
            catch (Exception) { }


        }

 
    }
}
