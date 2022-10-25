using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Application.MediatorRequestHandlers
{
    public class PopulateDatabaseCommand : IRequest
    {
    }

    public class PopulateDatabaseCommandHandler : IRequestHandler<PopulateDatabaseCommand>
    {
        private readonly WarehouseService warehouseService;
        public PopulateDatabaseCommandHandler(WarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public async Task<Unit> Handle(PopulateDatabaseCommand request, CancellationToken cancellationToken)
        {
            /*
             * NOTE
             * I decided to not create a method in the domain to manage this because it is only "example code".
             * In the domain doesn't exists something like "PopulateDatabase"
             * All these actions here force me to use both service and repository, it shouldn't happen in real code
             */
            Warehouse warehouse1 = await warehouseService.CreateWarehouse("Warehouse 1");
            Warehouse warehouse2 = await warehouseService.CreateWarehouse("Warehouse 2");

            /*
             * NOTE
             * based on your architecture you can decide to ask if productID is valid and can be load in the warehouse to other microservices
             * you can do it with a command, an http call to the gateway or you can think to have the product table in your DB
             * It really depends on the domain and the architecture
             */

            Guid productIdToMove = Guid.NewGuid();
            warehouse1.LoadProduct(productIdToMove, 5);
            warehouse2.LoadProduct(productIdToMove, 5);

            return Unit.Value;
        }
    }
}
