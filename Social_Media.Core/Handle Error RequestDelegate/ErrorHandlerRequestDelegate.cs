using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Core.Response_Structure;
using System.Net;
using System.Text.Json;

namespace Social_Media.Core.Handle_Error_RequestDelegate
{
    public class ErrorHandlerRequestDelegate
    {
        private readonly ILogger Logger;
        RequestDelegate Next;
        public ErrorHandlerRequestDelegate(RequestDelegate Next, ILogger Logger)
        {
            this.Next = Next;
            this.Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception error)
            {
                var Response = context.Response;
                Response.ContentType = "application/json";
                var ResponseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                //TODO:: cover all validation errors
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        // custom application error
                        ResponseModel.StatusCode = HttpStatusCode.Unauthorized;
                        Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        ResponseModel.Errors = e.InnerException == null ? null : new List<string> { e.InnerException.Message };
                        break;

                    case ValidationException e:
                        // custom validation error
                        ResponseModel.StatusCode = HttpStatusCode.BadRequest;
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        ResponseModel.Errors = e.InnerException == null ? null : new List<string> { e.InnerException.Message };
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        ResponseModel.StatusCode = HttpStatusCode.NotFound;
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        ResponseModel.Errors = e.InnerException == null ? null : new List<string> { e.InnerException.Message };
                        break;

                    case DbUpdateException e:
                        // can't update error
                        ResponseModel.StatusCode = HttpStatusCode.BadRequest;
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        ResponseModel.Errors = e.InnerException == null ? null : new List<string> { e.InnerException.Message };
                        break;
                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            ResponseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                            ResponseModel.StatusCode = HttpStatusCode.BadRequest;
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        ResponseModel.Errors = e.InnerException == null ? null : new List<string> { e.InnerException.Message };
                        ResponseModel.StatusCode = HttpStatusCode.BadRequest;
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        // unhandled error
                        ResponseModel.StatusCode = HttpStatusCode.BadRequest;
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                }
                var Result = JsonSerializer.Serialize(ResponseModel);
                Logger.Error(error, $"An error occurred while processing the request: {error.Message}");
                await Response.WriteAsync(Result);

            }
        }
    }
}
