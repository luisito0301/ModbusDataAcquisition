using ModbusData.Domain.Types;
using System;

namespace ModbusData.Domain.Entities.Variables
{
    /// <summary>
    /// Represents a digital variable.
    /// </summary>
    public class DigitalVariable : Variable
    {
        private short _value;

        /// <summary>
        /// Gets or sets the value of the digital variable.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not between 0 and 1024.</exception>
        public short Value
        {
            get => _value;
            set
            {
                if (value < 0 || value > 1024)
                    throw new ArgumentOutOfRangeException(nameof(Value), "El valor debe estar entre 0 y 1024.");
                _value = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the unit associated with the digital variable.
        /// </summary>
        

        public DigitalVariable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        {
        }

        // Required by Entity Framework
        protected DigitalVariable() { }
    }
}