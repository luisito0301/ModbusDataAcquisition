
using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Unit
{
    public class Unit : Entity
    {
      

        public int Id { get; set; }
        public string ManufactererName { get; set; }
        public string Code { get; set; }
        public string AreaName { get; set; }
        public List<Variable> Variables { get; set; } = new List<Variable>();

        // Método para agregar una variable a la unidad
        public void AddVariable(Variable variable)
        {
            if (Variables.Any(v => v.Id == variable.Id))
                throw new InvalidOperationException("La variable ya está asociada a esta unidad.");

            Variables.Add(variable);
        }
        public Unit(int id, string manufactererName, string code, string areaName, List<Variable> variables)
        {
            Id = id;
            ManufactererName = manufactererName;
            Code = code;
            AreaName = areaName;
            Variables = variables;
        }
    }
}
