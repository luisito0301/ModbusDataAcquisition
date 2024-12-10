
using ModbusData.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Variables
{
    public class DigitalVariable : Variable
    {
        private short _value;
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

        public Guid UnitID { get; set; } // Relación uno-muchos (variable-unidad)

        public DigitalVariable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        { }
        protected DigitalVariable() { }
    }

}
