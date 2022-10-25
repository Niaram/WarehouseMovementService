using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Infrastructure.Persistence
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public WarehouseRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        /*
         * NOTE
         * this methods: Add, Delete, GetByKey, ... can be added to a base class of T so that you don't have to repeat them every time
         */
        public async Task Add(Warehouse warehouse)
            => await applicationDBContext.Warehouses.AddAsync(warehouse);

        public async Task<Warehouse> GetByKey(Guid warehouseID)
        {
            Warehouse warehouse = await applicationDBContext
                                                        .Warehouses
                                                        .FirstOrDefaultAsync(w => w.ID == warehouseID);

            if (warehouse == null)
                throw new EntityNotFoundException($"Warehouse with id {warehouseID} not found");

            return warehouse;
        }

        public async Task<Warehouse?> FindWarehouseByCode(string code)
            => await applicationDBContext.Warehouses
                                            .Where(w => w.Code == code)
                                            .SingleOrDefaultAsync();
    }
}
