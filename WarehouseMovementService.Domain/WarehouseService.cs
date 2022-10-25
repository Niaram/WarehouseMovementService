using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Domain
{
    public class WarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        public async Task<Warehouse> CreateWarehouse(string code)
        {
            Warehouse? warehouse = await warehouseRepository.FindWarehouseByCode(code);

            if (warehouse != null)
                return warehouse;

            Warehouse newWarehouse = new Warehouse(code);
            await warehouseRepository.Add(newWarehouse);

            return newWarehouse;
        }
    }
}
