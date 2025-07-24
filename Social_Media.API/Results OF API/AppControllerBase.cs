using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Core.Response_Structure;

namespace Social_Media.API.Results_OF_API
{
    public class AppControllerBase : ControllerBase
    {
        public IMediator Mediator;
        public AppControllerBase(IMediator Mediator)
        {
            this.Mediator = Mediator;
        }
        public ObjectResult New_Result<T>(Response<T> Response)
        {
            switch (Response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return new OkObjectResult(Response);
                case System.Net.HttpStatusCode.Created:
                    return new CreatedResult("", Response);
                case System.Net.HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(Response);
                case System.Net.HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(Response);
                case System.Net.HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(Response);
                default:
                    return new BadRequestObjectResult(Response);
            }
        }
    }
}
