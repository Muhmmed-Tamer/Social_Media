using Microsoft.Extensions.Options;
using Social_Media.Data.Helpers.DataFromappSettings.Email;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using System.Net;
using System.Net.Mail;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.Email_Notification_Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSetting EmailSetting;
        private readonly Serilog.ILogger Logger;
        public EmailServices(Serilog.ILogger Logger, IOptions<EmailSetting> EmailSetting)
        {
            this.EmailSetting = EmailSetting.Value;
            this.Logger = Logger;
        }
        public async Task<bool> SendEmailAsync(string Subject, string Message, string EmailTo)
        {
            try
            {
                using (var Client = new SmtpClient())
                {
                    Client.Port = EmailSetting.Port;
                    Client.Credentials = new NetworkCredential(EmailSetting.EmailFrom, EmailSetting.Password);
                    Client.Host = EmailSetting.Host;
                    Client.EnableSsl = EmailSetting.EnableSsl;
                    await Client.SendMailAsync(CreateMailMessage(Subject, Message, EmailTo));
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, "Error When Send Email");
                throw;
            }
        }
        private MailMessage CreateMailMessage(string Subject, string Message, string EmailTo)
        {
            try
            {
                MailMessage MyMessage = new MailMessage()
                {
                    IsBodyHtml = EmailSetting.IsBodyHtml,
                    Subject = Subject,
                    Body = Message,
                    From = new MailAddress(EmailSetting.EmailFrom)
                };
                MyMessage.To.Add(EmailTo);
                return MyMessage;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
