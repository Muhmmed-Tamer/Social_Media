using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Serilog;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.ProtocolAndHosts_Services
{
    public class ProtocolAndHostServices : IProtocolAndHostServices
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly LinkGenerator LinkGenerator;
        private readonly ILogger Logger;
        public ProtocolAndHostServices(ILogger Logger, IHttpContextAccessor HttpContextAccessor, LinkGenerator LinkGenerator)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.LinkGenerator = LinkGenerator;
            this.Logger = Logger;
        }

        public string GetFullPathOFFile(string BaseDirectory, string? SubDirectory = null, string? FileName = null)
        {
            try
            {
                var FilePath = $"{GetProtocolAndHost()}{BaseDirectory}";
                if (!string.IsNullOrWhiteSpace(SubDirectory))
                {
                    FilePath += $"/{SubDirectory.Trim('/')}";
                }
                if (!string.IsNullOrWhiteSpace(FileName))
                {
                    FilePath += $"/{FileName.Trim('/')}";
                }
                return FilePath;

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetFullPathOFFile method");
                throw;
            }
        }

        /// <summary>
        /// I add / when Send Subdomain Null.
        /// </summary>
        /// <returns></returns>        
        public string GetProtocolAndHost()
        {
            try
            {
                return $"{HttpContextAccessor.HttpContext?.Request.Scheme}://{HttpContextAccessor.HttpContext?.Request.Host}/";
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetProtocolAndHost method");
                throw;
            }
        }

        public string GoToEndPointInMyApplication(string Controller, string Action, object? Data)
        {
            try
            {
                if (Data is not null)
                {
                    return $"{GetProtocolAndHost().Trim('/')}{LinkGenerator.GetPathByAction(controller: Controller, action: Action, values: Data)}";
                }
                return $"{GetProtocolAndHost().Trim('/')}{LinkGenerator.GetPathByAction(controller: Controller, action: Action)}";
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetToEndPointInMyApplication method");
                throw;
            }
        }
    }
}
