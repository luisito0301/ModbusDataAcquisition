
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
        public short Value { get; set; }
        public Guid UnitID { get; set; }  ///Estableciendo relacion uno-muchos(variable-unidad)
        public DigitalVariable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress) : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        {}
    }
}
