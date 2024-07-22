using Amazon.S3;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Responses;
using System.Net;
using System.Text.Json;

namespace LibraryManager.Api.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandlerAsync(httpContext, ex);
                Console.WriteLine(ex);
            }
        }

        public async Task HandlerAsync(HttpContext httpContext, Exception ex)
        {
            string operation = string.Empty;
            string message = string.Empty;
            HttpStatusCode statusCode;

            switch (ex)
            {
                case KeyNotFoundException:
                    message = ex.Message;
                    statusCode = HttpStatusCode.NotFound;
                    operation = EOperationType.GetById.ToString();
                    break;

                case AmazonS3Exception:
                    message = ex.Message;
                    if (httpContext.Request.Method == "POST")
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        operation = EOperationType.Create.ToString();
                    }
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

                default:
                    message = "An unexpected error ocorred";
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;

            var apiResponse = new APIResponse<Exception>(operation, false, (int)statusCode, message);

            var serializedResponse = JsonSerializer.Serialize(apiResponse);

            await httpContext.Response.WriteAsync(serializedResponse);
        }
    }
}
