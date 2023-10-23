using System;



namespace Framework.Security2023.IServices
{
    interface IServiceEmailSecurity
    {
        void SendEmailForgetPassword(string userName,
            string emailTo,string url, Guid userId);

        bool EmailValidForgetPassword(Guid userId, Guid idRequest);

    }
}
