using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiDapper.ConfigureExceptionHandler;

namespace WebApiDapper.ExceptionFilters
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseExceptionHandler(appError =>
            //{
            //    appError.Run(async (context) =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync(new ExceptionModels.ExceptionModel
            //        {
            //            Message = "Internal Server Error.",
            //            StatusCode = context.Response.StatusCode,
            //        }.ToString());
            //    });
            //});
        }
    }
}
