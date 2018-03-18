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

        private void HasChanges()
        {
            this.NeedsPersistence = true;
        }
    }
}
