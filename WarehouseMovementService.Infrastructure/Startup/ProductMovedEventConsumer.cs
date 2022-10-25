using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Infrastructure
{
    public class ProductMovedEventConsumer : IConsumer<ProductLoadedEvent>
    {
        private ILogger<ProductMovedEventConsumer> logger;
        public ProductMovedEventConsumer(ILogger<ProductMovedEventConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductLoadedEvent> context)
        {
            await Task.CompletedTask;
        }
    }
}
