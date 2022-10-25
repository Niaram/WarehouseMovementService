using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Infrastructure.Persistence;

namespace WarehouseMovementService.Application.ReadModel
{
    public interface IReadWarehouseAvailabilityRepository
    {
        Task<List<GetWarehouseAvailabilitiesModel>> GetWarehouseAvailabilities();
    }

    public class ReadWarehouseAvailabilityRepository : IReadWarehouseAvailabilityRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public ReadWarehouseAvailabilityRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<List<GetWarehouseAvailabilitiesModel>> GetWarehouseAvailabilities()
            => await applicationDBContext
                            .Warehouses
                            .Include(w => w.Availabilities)
                            .SelectMany(w => w.Availabilities)
                            .Select(a => new GetWarehouseAvailabilitiesModel()
                            {
                                WarehouseID = a.Warehouse.ID,
                                ProductID = a.ProductID,
                                Quantity = a.Quantity,
                                State = a.State.Description
                            })
                            .ToListAsync();
    }
}
