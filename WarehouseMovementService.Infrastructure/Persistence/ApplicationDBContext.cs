using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options,
                                    IBus bus) : base(options)
        {
            Bus = bus;
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public IBus Bus { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WarehouseMapping());
            modelBuilder.ApplyConfiguration(new WarehouseAvailabilityMapping());

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            if (HasAggregateToCommit() == false)
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await PublishDomainEvents();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private async Task PublishDomainEvents()
        {
            List<Aggregate> aggregates = GetAggregatesToCommit();
            foreach (var domainEvent in aggregates.SelectMany(d => d.Events))
                await Bus.Publish(domainEvent);
        }

        public List<Aggregate> GetAggregatesToCommit()
                        => ChangeTracker
                                .Entries()
                                .Select(e => e.Entity)
                                .OfType<Aggregate>()
                                .Select(e => (Aggregate)e)
                                .ToList();
        public bool HasAggregateToCommit()
            => GetAggregatesToCommit() != null && GetAggregatesToCommit().Any();
    }
}
