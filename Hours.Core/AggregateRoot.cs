using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core
{
    /// <summary>
    /// A base class for defining aggregate roots.
    /// </summary>
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            this.AggregateVersion = 0;
            this.NeedsPersistence = false;
        }

        /// <summary>
        /// Gets the aggregate version, used for optistimic offline locking.
        /// </summary>
        /// <remarks>
        /// see https://martinfowler.com/eaaCatalog/optimisticOfflineLock.html
        /// </remarks>
        public int AggregateVersion { get; protected set; }
        
        /// <summary>
        /// Gets the if the aggregate needs to be persisted (an update
        /// has been made).
        /// </summary>
        public bool NeedsPersistence { get; protected set; }

        /// <summary>
        /// Marks the aggre
        /// </summary>
        protected void HasChanges()
        {
            if(!this.NeedsPersistence)
            {
                this.NeedsPersistence = true;
                this.AggregateVersion++;
            }           
        }

        /// <summary>
        /// C# event which is called when a DomainEvent occurs in the aggregate.
        /// </summary>
        public event EventHandler<DomainEvent> RaiseDomainEvent;

        /// <summary>
        /// Raises a domain event if it's been subscribed to.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected virtual void OnRaiseDomainEvent(DomainEvent domainEvent)
        {
            // make a copy of the handle to help with race conditions
            // see 
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
            var handler = this.RaiseDomainEvent;

            if(handler != null)
            {
                handler(this, domainEvent);
            }
        }
    }
}
