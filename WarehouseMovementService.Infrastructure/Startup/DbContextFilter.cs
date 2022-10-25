using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Infrastructure
{
    public class DbContextFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly ApplicationDBContext dbContext;

        public DbContextFilter(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);

            await dbContext.SaveChangesAsync();
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}
