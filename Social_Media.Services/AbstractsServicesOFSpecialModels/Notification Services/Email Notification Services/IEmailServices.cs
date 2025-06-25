namespace Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services
{
    public interface IEmailServices
    {
        Task<bool> SendEmailAsync(string Subject, string Message, string ToEmail);
    }
}
