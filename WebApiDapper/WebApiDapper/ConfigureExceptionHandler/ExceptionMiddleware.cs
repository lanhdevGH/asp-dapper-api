using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiDapper.ExceptionFilters.ExceptionModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApiDapper.ConfigureExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate request)
        {
            _next = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ExceptionModel
            {
                Message = "Internal Server Error from the custom middleware",
                StatusCode = context.Response.StatusCode,
            }.ToString());
        }
    }
}
