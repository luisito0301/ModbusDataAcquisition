using System;

namespace ModbusData.Domain.Types
{
    /// <summary>
    /// Represents the type of a variable.
    /// </summary>
    public enum VariableType
    {
        /// <summary>
        /// Represents an analog variable.
        /// </summary>
        Analogic,

        /// <summary>
        /// Represents a digital variable.
        /// </summary>
        Digital
    }
}