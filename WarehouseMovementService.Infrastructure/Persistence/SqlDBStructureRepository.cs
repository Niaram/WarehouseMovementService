using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Infrastructure.Persistence
{
    internal class SqlDBStructureRepository : IDBStructureRespository
    {
        private readonly ApplicationDBContext dbContext;

        public SqlDBStructureRepository(ApplicationDBContext dbContext)
            => this.dbContext = dbContext;

        public async Task EnsureCreated()
            => await dbContext.Database.EnsureCreatedAsync();
    }

    public interface IDBStructureRespository
    {
        Task EnsureCreated();
    }
}
