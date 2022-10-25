using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Domain
{
    public class WarehouseAvailability : Entity
    {
        public WarehouseAvailability() { }

        protected internal WarehouseAvailability(Warehouse warehouse, Guid productID, int quantity, WarehouseProductState state) : base()
        {
            Warehouse = warehouse;
            ProductID = productID;
            Quantity = quantity;
            State = state;
        }

        public virtual Warehouse Warehouse { get; private set; }
        public Guid ProductID { get; private set; }
        public int Quantity { get; private set; }
        public WarehouseProductState State { get; private set; }

        protected internal void Load(int quantityToLoad)
            => Quantity = Quantity + quantityToLoad;
    }
}
