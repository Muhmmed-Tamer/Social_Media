using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class ExternalNotificationUnitOFWork : IExternalNotificationUnitOFWork
    {
        public ExternalNotificationUnitOFWork(IEmailServices EmailServices, ISMSServices SMSServices)
        {
            this.EmailServices = EmailServices;
            this.SMSServices = SMSServices;
        }

        public IEmailServices EmailServices { get; }

        public ISMSServices SMSServices { get; }
    }
}
