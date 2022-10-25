using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Application
{
    public class GetWarehouseAvailabilitiesModel
    {
        public Guid WarehouseID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public string State { get; set; }
    }
}
