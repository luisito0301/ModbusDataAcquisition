using ModbusData.Domain.Common;
using ModbusData.Domain.Records;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Domain.Entities.Variables
{
    /// <summary>
    /// Base class for different types of variables.
    /// </summary>
    public abstract class Variable : Entity
    {
        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; init; } // Nombre de la variable

        /// <summary>
        /// Gets the type of the variable (Analogic or Digital).
        /// </summary>
        public VariableType Type { get; init; } // Tipo: Analógica o Digital

        /// <summary>
        /// Gets a value indicating whether the variable is a measurement.
        /// </summary>
        public bool IsMeasurement { get; init; } // true si es medición, false si es acción de control

        /// <summary>
        /// Gets the code associated with the variable.
        /// </summary>
        public string Code { get; init; } // Código de la variable

        /// <summary>
        /// Gets the sampling period for the variable.
        /// </summary>
        public TimeSpan SamplingPeriod { get; init; } // Intervalo de tiempo

        /// <summary>
        /// Gets the Modbus address for the variable.
        /// </summary>
        public int ModbusAddress { get; init; } // Dirección en el protocolo ModBus
        public Guid UnitId { get; set; } // Relación uno-muchos (variable-unidad)
        public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
        protected Variable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id)
        {
            Name = name;
            Type = type;
            IsMeasurement = isMeasurement;
            Code = code;
            SamplingPeriod = samplingPeriod;
            ModbusAddress = modbusAddress;
        }

        // Required by Entity Framework
        protected Variable() { }

    }
}