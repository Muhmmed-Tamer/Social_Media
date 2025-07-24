using Microsoft.Extensions.Options;
using Serilog;
using Social_Media.Data.Helpers.DataFromappSettings.SMS;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.SMS_Notification_Services
{
    public class SMSServices : ISMSServices
    {
        private readonly SMSSetting SMSSetting;
        private readonly ILogger Logger;
        public SMSServices(ILogger Logger, IOptions<SMSSetting> SMSSetting)
        {
            this.SMSSetting = SMSSetting.Value;
            this.Logger = Logger;
        }
        public async Task<bool> SendSMSAsync(string Message, string PhoneNumberTo)
        {
            try
            {
                TwilioClient.Init(SMSSetting.AccountSID, SMSSetting.AuthToken);
                var Result = await MessageResource.CreateAsync(body: Message, from: new PhoneNumber(SMSSetting.PhoneNumberFrom), to: PhoneNumberTo);
                //TaskStatus
                if (Result.Status == MessageResource.StatusEnum.Accepted || Result.Status == MessageResource.StatusEnum.Queued || Result.Status == MessageResource.StatusEnum.Sent || Result.Status == MessageResource.StatusEnum.Delivered)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
