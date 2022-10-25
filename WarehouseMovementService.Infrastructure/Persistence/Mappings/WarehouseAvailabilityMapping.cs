using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Infrastructure.Persistence.DDD;

namespace WarehouseMovementService.Infrastructure.Persistence
{
    public class WarehouseAvailabilityMapping : EntityMapping<WarehouseAvailability>
    {
        public override void Configure(EntityTypeBuilder<WarehouseAvailability> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.ProductID);
            builder.Property(c => c.Quantity);

            builder
                .Property(c => c.State)
                .HasConversion(state => state.ID,
                                id => WarehouseProductStateList.GetById(id));

            builder.HasOne(c => c.Warehouse);
        }
    }
}
