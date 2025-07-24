namespace Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services
{
    public interface ISMSServices
    {
        Task<bool> SendSMSAsync(string Message, string PhoneNumberTo);
    }
}
