using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Domain.DDD
{
    public abstract class Entity
    {
        public Entity()
        {
            ID = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid ID { get; private set; }
        public DateTime CreationDate { get; private set; }

    }
}
