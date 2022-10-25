using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Domain
{
    public class Warehouse : Aggregate
    {
        protected internal Warehouse(string code) : base()
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            availabilities = new List<WarehouseAvailability>();
        }

        public string Code { get; set; }

        private ICollection<WarehouseAvailability> availabilities = new List<WarehouseAvailability>();
        public virtual IEnumerable<WarehouseAvailability> Availabilities => availabilities;

        public virtual void LoadProduct(Guid productID, int quantity)
        {
            WarehouseAvailability? productAvailability = FindProductAvailability(productID);

            if (productAvailability != null)
                productAvailability.Load(quantity);
            else
                AddProductAvailability(productID, quantity);

            RaiseEvent(new ProductLoadedEvent(ID, productID, quantity));
        }

        private WarehouseAvailability AddProductAvailability(Guid productID, int quantity)
        {
            WarehouseAvailability availability = new WarehouseAvailability(this, productID, quantity, WarehouseProductStateList.Available);
            availabilities.Add(availability);

            return availability;
        }

        private WarehouseAvailability? FindProductAvailability(Guid productID)
            => availabilities.SingleOrDefault(a => a.ProductID == productID);
    }
}
