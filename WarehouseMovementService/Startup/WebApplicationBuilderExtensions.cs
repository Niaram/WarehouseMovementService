using Serilog;
using System.Reflection;

namespace WarehouseMovementService.Startup
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder UseWebApplication(this WebApplicationBuilder builder, params Assembly[] mediatorAssemblies)
        {
            builder.Host.UseSerilog(); 
            /* NOTE
             * configure serilog to log where it's more usefull (Database, file, ...)
             * maybe log in a file can be ok to send, every X time the log to the centralized log service
             */

            builder.Services.AddWebApplication(mediatorAssemblies);

            return builder;
        }
    }
}
