
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
        
        public string ManufactererName { get; set; } ///Nombre del fabricante
        public string Code { get; set; }  ///Codigo asociado a la unidad
        public string AreaName { get; set; } ///Nombre del area donde se encuentran
       private List<Variable> _variables { get; set; } = new List<Variable>(); ///Variables asociadas a la unidad
        public IReadOnlyCollection<Variable> Variables
        { 
            get { return _variables; }
            protected set { _variables = value.ToList(); }
        }
        /// Método para agregar una variable a la unidad
        public void AddVariable(Variable variable)
        {
            if (_variables.Any(v => v.Id == variable.Id))
                throw new InvalidOperationException("La variable ya está asociada a esta unidad.");

            _variables.Add(variable);
        }
        public void RemoveVariable(Variable variable)
        {  _variables.Remove(variable); }
        public Unit(Guid id, string manufactererName, string code, string areaName, List<Variable> variables): base(id)
        {
           
            ManufactererName = manufactererName;
            Code = code;
            AreaName = areaName;
            _variables = variables;
        }
        ///<summary>
        ///Requerido por EntityFramework
        ///<summary>
        protected Unit() { }
    }
}
