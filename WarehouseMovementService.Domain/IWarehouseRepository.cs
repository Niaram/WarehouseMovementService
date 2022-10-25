using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Domain
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> FindWarehouseByCode(string code);

        Task<Warehouse> GetByKey(Guid warehouseID);

        Task Add(Warehouse warehouse);
    }
}
