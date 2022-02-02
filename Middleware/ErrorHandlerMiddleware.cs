using Microsoft.AspNetCore.Http;
using SmartMeterControl.Access_MS.Resources;
using System;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ResponseMessage<string>.Fail(context.Response.StatusCode,error.Message);
                string result = System.Text.Json.JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }

    }
}
