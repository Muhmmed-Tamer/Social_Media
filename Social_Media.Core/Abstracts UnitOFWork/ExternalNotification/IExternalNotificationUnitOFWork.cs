using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IExternalNotificationUnitOFWork
    {
        public IEmailServices EmailServices { get; }

        public ISMSServices SMSServices { get; }
    }
}
