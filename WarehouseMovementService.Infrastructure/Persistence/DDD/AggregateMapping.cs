using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Infrastructure.Persistence.DDD
{
    public class AggregateMapping<T> : EntityMapping<T>
        where T : Aggregate
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Version).IsRowVersion();

            builder.Ignore(c => c.Events);
        }
    }
}
