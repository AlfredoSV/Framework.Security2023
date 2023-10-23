using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;

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
                MailMessage message = new MailMessage();
                message.From = new MailAddress(SmtpConfigurationSecurityFkw.Instance.UserName);

                message.To.Add(emailTo);
                Dictionary<string, string> paramsBody = new Dictionary<string, string>
                {
                    { "@userName", userName }
                };
                Guid idTemplate = Guid.Parse("81D995AC-8690-4960-8292-80BAF046736A");

                message.Body = GenerateBody(paramsBody, idTemplate);

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
            catch (Exception)
            {
            }
        }
        void IServiceEmailSecurity.SendEmailForgetPassword(string userName,
            string emailTo, string url)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(SmtpConfigurationSecurityFkw.Instance.UserName);

                message.To.Add(emailTo);
                Dictionary<string, string> paramsBody = new Dictionary<string, string>
                {
                    { "@userName", userName },
                    { "@url", url }
                };
                Guid idTemplate = Guid.Parse("4479E1C7-E459-44CB-BB9E-93C158454CC2");

                message.Body = GenerateBody( paramsBody, idTemplate);

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
