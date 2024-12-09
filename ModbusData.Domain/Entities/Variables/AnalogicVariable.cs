
using ModbusData.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Variables
{
    public class AnalogicVariable : Variable
    {
        private double _value;
        public double Value
        {
            get => _value;
            set => _value = Math.Round(value, 2);
        }

        public Guid UnitID { get; set; } // Relación uno-muchos (variable-unidad)

        public AnalogicVariable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
            : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        { }
        protected AnalogicVariable() { }
    }

}
