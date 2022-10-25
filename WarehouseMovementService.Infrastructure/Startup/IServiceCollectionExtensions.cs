using WarehouseMovementService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using System.Reflection;
using WarehouseMovementService.Domain;

namespace WarehouseMovementService.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection thisServiceCollection,
                                                            string connectionString,
                                                            params Assembly[] consumerAssemblies)
            => thisServiceCollection
                            .AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString))
                            .AddTransient<IDBStructureRespository, SqlDBStructureRepository>()
                            .AddMassTransit(x =>
                            {
                                x.AddEntityFrameworkOutbox<ApplicationDBContext>(o =>
                                {
                                    o.QueryDelay = TimeSpan.FromSeconds(1);

                                    o.UseSqlServer();
                                    o.UseBusOutbox();
                                });

                                x.AddConsumers(consumerAssemblies);

                                /* NOTE
                                 * useRabbitMQ or something else there.
                                 * InMemory bus allows you to use the code as is without dependencies
                                 */
                                x.UsingInMemory((context, cfg) =>
                                {
                                    cfg.UseConsumeFilter(typeof(DbContextFilter<>), context);
                                    cfg.ConfigureEndpoints(context);
                                });
                            })
                            .AddScoped<IWarehouseRepository, WarehouseRepository>()
                            .AddScoped<WarehouseService, WarehouseService>();
                            /*
                            * NOTE
                            * here you can write code in order to load all the repositories and services that inherits from a base class and register them.
                            * It is nice because you don't have to remember to add classes when you create it.
                            * Also you can avoid to add service (domain class) from here (infrastructure).
                            */
    }
}
