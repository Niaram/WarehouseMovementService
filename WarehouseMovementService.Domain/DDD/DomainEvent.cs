using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Domain.DDD
{
    public abstract class DomainEvent
    {
        public DomainEvent()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
    }
}
