using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Infrastructure.Persistence.DDD
{
    public class EntityMapping<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedNever();
            builder.Property(c => c.CreationDate);
        }
    }
}
