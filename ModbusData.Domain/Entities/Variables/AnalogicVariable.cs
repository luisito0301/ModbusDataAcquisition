
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
        public Guid UnitID { get; set; }
        public AnalogicVariable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress) : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
        {
        }
       
        public override decimal GetSample()
        {
            // Lógica para obtener la muestra de una variable analógica
            // Simulamos un valor de muestra entre 0 y 100, con dos cifras decimales
            Random random = new Random();
            decimal valor = Math.Round((decimal)(random.NextDouble() * 100), 2);
            return valor;
        }
    }
}
