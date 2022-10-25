using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Domain
{
    public static class WarehouseProductStateList
    {
        public static WarehouseProductState Booked => new WarehouseProductState(1, "Booked");
        public static WarehouseProductState Available => new WarehouseProductState(2, "Available");

        public static List<WarehouseProductState> All => new List<WarehouseProductState>() { Booked, Available };

        public static WarehouseProductState GetById(byte id)
        {
            WarehouseProductState? state = All.FirstOrDefault(s => s.ID == id);

            if (state == null)
                throw new DomainException($"State with id {id} not found");

            return state;
        }
    }

    public class WarehouseProductState
    {
        public WarehouseProductState(byte id, string description)
        {
            ID = id;
            Description = description;
        }

        public byte ID { get; set; }
        public string Description { get; set; }
    }
}
