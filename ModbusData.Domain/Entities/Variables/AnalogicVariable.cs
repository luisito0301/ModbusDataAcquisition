using ModbusData.Domain.Types;
using System;

namespace ModbusData.Domain.Entities.Variables
{
    /// <summary>
    /// Represents an analog variable.
    /// </summary>
    public class AnalogicVariable : Variable
    {
      

        /// <summary>
        /// Gets or sets the value of the analog variable, rounded to two decimal places.
        /// </summary>
       

        /// <summary>
        /// Gets or sets the identifier of the unit associated with the analog variable.
        /// </summary>
        

        public AnalogicVariable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        { }

        // Required by Entity Framework
        protected AnalogicVariable() { }

    }
}