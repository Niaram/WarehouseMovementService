using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Reflection;
using WarehouseMovementService.Application;
using WarehouseMovementService.Application.MediatorRequestHandlers;
using WarehouseMovementService.Infrastructure;
using WarehouseMovementService.Startup;

namespace WarehouseMovementService.Test
{
    /*
     * Readme: before run the test please change the connectionstring in appsettings.json 
     */
    [TestClass]
    public class PopulateDatabaseTest
    {
        [TestMethod]
        public async Task PopulateDatabase_NameOfTheStartingSituation()
        {
            var configuration = new ConfigurationBuilder()
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();


            var applicationLayerAssembly = Assembly.Load("WarehouseMovementService.Application");
            string connectionString = configuration.GetConnectionString("DefaultConnectionString");
            ServiceProvider serviceProvider = new ServiceCollection()
                                                                .AddWebApplication(applicationLayerAssembly)
                                                                .AddApplication()
                                                                .AddInfrastructure(connectionString, applicationLayerAssembly)
                                                                .BuildServiceProvider();

            await serviceProvider.EnsureDBCreated();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new PopulateDatabaseCommand());
            List<GetWarehouseAvailabilitiesModel> availabilitiesModels = await mediator.Send(new GetWarehouseAvailabilitiesQuery());

            availabilitiesModels
                    .Count(a => a.Quantity == 5)
                    .ShouldBe(2);
        }
    }
}