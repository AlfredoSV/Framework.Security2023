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

         void IServiceEmailSecurity.EmailValidForgetPassword(string userName,
            string emailTo)
        {
            try
            {
                Guid idTemplate = SmtpConfigurationSecurityFkw.Instance.idTemplateEmailValidForgetPassword;//Guid.Parse("81D995AC-8690-4960-8292-80BAF046736A");
                Dictionary<string, string> paramsBody = new Dictionary<string, string>
                {
                    { "@userName", userName }
                };
                MailMessage message = new MailMessage();
                message.From = new MailAddress(SmtpConfigurationSecurityFkw.Instance.UserName);
                message.To.Add(emailTo);
                message.Body = GenerateBody(paramsBody, idTemplate);
                using (SmtpClient smtpClient = new SmtpClient(SmtpConfigurationSecurityFkw.Instance.Host))
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
            catch (Exception)
            {
                throw;
            }
        }
        void IServiceEmailSecurity.SendEmailForgetPassword(string userName,
            string emailTo, string url)
        {
            try
            {
                Guid idTemplate = SmtpConfigurationSecurityFkw.Instance.idTemplateSendEmailForgetPassword; //Guid.Parse("4479E1C7-E459-44CB-BB9E-93C158454CC2");
                Dictionary<string, string> paramsBody = new Dictionary<string, string>
                {
                    { "@userName", userName },
                    { "@url", url }
                };
                MailMessage message = new MailMessage();
                message.From = new MailAddress(SmtpConfigurationSecurityFkw.Instance.UserName);
                message.To.Add(emailTo);                
                message.Body = GenerateBody( paramsBody, idTemplate);
                using (SmtpClient smtpClient = new SmtpClient(SmtpConfigurationSecurityFkw.Instance.Host))
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
            catch (Exception) 
            {
                throw;
            }


        }

        private string GenerateBody(Dictionary<string, string> paramsBody,
          Guid idTemplate)
        {
            TemplateEmail template = _repositoryTemplatesEmail.GetByid(idTemplate);

            foreach (KeyValuePair<string, string> paraBo in paramsBody)
            {
                template.BodyTemplate = template.BodyTemplate.Replace(paraBo.Key, paraBo.Value);
            }

            return template.BodyTemplate;
        }

    }
}
