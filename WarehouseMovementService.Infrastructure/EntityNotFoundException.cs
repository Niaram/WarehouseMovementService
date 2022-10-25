using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Infrastructure
{
    public class EntityNotFoundException : Exception
    {
        /*
         * NOTE
         * This is needed if you want to make something special on entity not found exception (like return 404)
         */
        public EntityNotFoundException(string message) : base(message) { }
    }
}
