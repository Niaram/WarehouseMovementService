using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Infrastructure.Persistence.DDD;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace WarehouseMovementService.Infrastructure.Persistence
{
    public class WarehouseMapping : AggregateMapping<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            base.Configure(builder);

            builder
                .Property(c => c.Code)
                .HasMaxLength(200);
            builder
                .HasIndex(c => c.Code)
                .IsUnique();

            builder
                .HasMany(c => c.Availabilities)
                .WithOne(c => c.Warehouse)
                .HasForeignKey("WarehouseID");
        }
    }
}
