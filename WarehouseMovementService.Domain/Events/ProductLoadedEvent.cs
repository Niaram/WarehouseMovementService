using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Domain
{
    public class ProductLoadedEvent : DomainEvent
    {
        public ProductLoadedEvent(Guid warehouseID, Guid productID, int quantity) : base()
        {
            WarehouseID = warehouseID;
            ProductID = productID;
            Quantity = quantity;
        }

        public Guid WarehouseID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
