using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMovementService.Domain.DDD
{
    public abstract class Aggregate : Entity
    {
        public virtual byte[] Version { get; protected set; }

        public virtual IEnumerable<DomainEvent> Events => events;
        protected List<DomainEvent> events = new List<DomainEvent>();

        protected virtual void RaiseEvent(DomainEvent theEvent)
        {
            if (IsEventAlreadyPresent(theEvent))
                throw new DomainException($"Event {theEvent.ID} already present in aggregate {ID}. You can't add the event twice");

            events.Add(theEvent);
        }

        private bool IsEventAlreadyPresent(DomainEvent theEvent)
            => events != null && events.Any(e => e.ID == theEvent.ID);
    }
}
