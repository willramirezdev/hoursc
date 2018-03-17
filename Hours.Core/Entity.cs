using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core
{
    /// <summary>
    /// Base class for declared entities.
    /// </summary>
    /// <remarks>
    /// See http://enterprisecraftsmanship.com/2014/11/08/domain-object-base-class/
    /// </remarks>
    public abstract class Entity
    {
        public virtual long Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if(compareTo is null)
            {
                return false;
            }

            if(ReferenceEquals(this, compareTo))
            {
                return true;
            }   

            if(
                !this.IsTransient() && 
                !compareTo.IsTransient() && 
                this.Id == compareTo.Id)
            {
                return true;
            }

            return false;
        }

        public virtual bool IsTransient()
        {
            return this.Id == 0;
        }

        public static bool operator == (Entity a, Entity b)
        {
            if(a is null && b is null)
            {
                return true;
            }

            if(a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator != (Entity a, Entity b)
        {
            return !(a == b);
        }
    }
}
