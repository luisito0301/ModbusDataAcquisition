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
       

        

        public DigitalVariable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        {
        }

        // Required by Entity Framework
        protected DigitalVariable() { }
    }
}