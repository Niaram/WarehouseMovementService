using MediatR;
using Serilog;
using WarehouseMovementService.Application;
using WarehouseMovementService.Application.MediatorRequestHandlers;
using WarehouseMovementService.Domain.DDD;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.APIs
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapAPI(this WebApplication thisWebApplication)
        {
            thisWebApplication
                .MapGet("/warehouseAvailabilities",
                        async (IMediator mediatr) => await mediatr.Send(new GetWarehouseAvailabilitiesQuery()))
                .WithName("GetWarehouseAvailabilities")
                .Produces<List<GetWarehouseAvailabilitiesModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            /*
             * NOTE
             * this is a fake API only to let you know how i like to manage write model
             */
            thisWebApplication
                .MapPost("/populateDatabase",
                        async (IMediator mediatr) => await mediatr.Send(new PopulateDatabaseCommand()))
                .WithName("PopulateDatabase")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            return thisWebApplication;
        }
    }
}
