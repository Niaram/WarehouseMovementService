using MediatR;
using System.Reflection;

namespace WarehouseMovementService.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApplication(this IServiceCollection thisServiceCollection, params Assembly[] mediatorAssemblies) 
        {
            thisServiceCollection.AddAuthentication();

            thisServiceCollection.AddMediatR(mediatorAssemblies);

            return thisServiceCollection;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection thisServiceCollection) 
        {
            /*
             * add the services needed to implement the authentication.
             * Serices to add are based on the identity server we want to use.
             */
            return thisServiceCollection;
        }
    }
}
