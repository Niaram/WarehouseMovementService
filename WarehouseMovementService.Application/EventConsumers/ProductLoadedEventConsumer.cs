using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Application
{
    public class ProductLoadedEventConsumer : IConsumer<ProductLoadedEvent>
    {
        public ProductLoadedEventConsumer()
        {
        }

        public async Task Consume(ConsumeContext<ProductLoadedEvent> context)
        {
            //here management of possible "internal events"
            await Task.CompletedTask;
        }
    }
}
