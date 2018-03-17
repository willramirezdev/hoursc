using System;
using System.Collections.Generic;
using System.Linq;

namespace Hours.Core
{
    /// <summary>
    /// A base class for declaring value objects.
    /// </summary>
    /// <remarks>
    /// see http://enterprisecraftsmanship.com/2017/08/28/value-object-a-better-implementation/
    /// </remarks>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if(this.GetType() != obj.GetType())
            {
                throw new ArgumentException($"Invalid comparison of Value Objects of different types: {GetType()} and {obj.GetType()}");
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public static bool operator == (ValueObject a, ValueObject b)
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

        public static bool operator != (ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return this
                .GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }
    }
}
