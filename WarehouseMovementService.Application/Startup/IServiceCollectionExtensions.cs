using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using System.Reflection;
using WarehouseMovementService.Application.ReadModel;

namespace WarehouseMovementService.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection thisServiceCollection)
            => thisServiceCollection
                            .AddScoped<IReadWarehouseAvailabilityRepository, ReadWarehouseAvailabilityRepository>();
        /*
         * NOTE
         * here you can write code in order to load all the repositories that inherits from a IReadRepositoryBase and register them.
         * It is nice because you don't have to remember to add a repository when you create it
         */
    }
}
