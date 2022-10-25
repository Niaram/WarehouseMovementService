using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Runtime.CompilerServices;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Startup
{
    public static class IServiceProviderExtensions
    {
        public static async Task<IServiceProvider> EnsureDBCreated(this IServiceProvider thisServiceProvider)
        {
            using (var scope = thisServiceProvider.CreateScope())
            {
                var respository = scope.ServiceProvider.GetRequiredService<IDBStructureRespository>();
                await respository.EnsureCreated();
            }

            return thisServiceProvider;
        }
    }
}
