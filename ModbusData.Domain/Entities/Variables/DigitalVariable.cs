
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
        public Guid UnitID { get; set; }
        public DigitalVariable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress) : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        {
           
        }

        public override decimal GetSample()
        {
            // Lógica para obtener la muestra de una variable digital
            // Simulamos un valor entre 0 y 1024
            Random random = new Random();
            return random.Next(0, 1025); // Valores entre 0 y 1024
        }
    }
}
