using System.Net;
using Contracts;
using Entities.ErrorModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace PublicAddressBook.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    if (feature != null)
                    {
                        logger.LogError($"Error occurred: {feature.Error} ");
                        await context.Response.WriteAsync(
                            new ErrorDetails { Message = "Internal Server Error", StatusCode = context.Response.StatusCode }.ToString());
                    }
                });
            });
        }
    }
}