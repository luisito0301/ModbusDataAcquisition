using System;
using System.Collections.Generic;
using System.Linq;

namespace ModbusData.Domain.Common
{
    /// <summary>
    /// Base class for all value objects.
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the equality components
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0) // Handle nulls
                .Aggregate((x, y) => x ^ y); // XOR to combine hash codes
        }
    }
}