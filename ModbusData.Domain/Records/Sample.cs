using ModbusData.Domain.Entities.Variables;
using System;

namespace ModbusData.Domain.Records
{
    /// <summary>
    /// Represents a sample associated with a variable.
    /// </summary>
    public record Sample
    {
        /// <summary>
        /// Gets the identifier of the variable associated with the sample.
        /// </summary>
        public Guid VariableId { get; init; }

        /// <summary>
        /// Gets the date when the sample was taken.
        /// </summary>
        public DateTime Date { get; init; }

        // Optionally, you could add a value property if needed
         public double Value { get; init; }

    }
}