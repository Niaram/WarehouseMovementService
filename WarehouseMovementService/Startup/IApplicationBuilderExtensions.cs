using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json.Serialization;
using WarehouseMovementService.Domain.DDD;
using static System.Net.Mime.MediaTypeNames;

namespace WarehouseMovementService.Startup
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWebApplication(this IApplicationBuilder thisApplicationBuilder)
           => thisApplicationBuilder
                       .UseExceptionHandler(exceptionHandlerApp =>
                       {
                           exceptionHandlerApp.Run(async context =>
                           {
                               var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                               context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                               /* NOTE
                                * manage exceptions here in order to return a structure that Front end can use to translate and show messages
                                */
                               if (exceptionHandlerPathFeature != null)
                               {
                                   context.Response.ContentType = Text.Plain;
                                   await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
                               }
                           });
                       })
                       .UseSerilogRequestLogging()
                       .UseAuthentication()
                       .UseDDDUnitOfWork();

        public static IApplicationBuilder UseAuthentication(this IApplicationBuilder thisApplicationBuilder)
        {
            /* NOTE
             * add the middleware needed to implement the authentication.
             * Middleware to add are based on the identity server we want to use.
             */
            return thisApplicationBuilder;
        }

        public static IApplicationBuilder UseDDDUnitOfWork(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<DDDUnitOfWorkMiddleware>();
    }
}
