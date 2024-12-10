using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.Variables
{
    public interface IVariableRepository
    {
        void AddVariable(Variable variable);
        void DeleteVariable(Variable variable);
        IEnumerable<T> GetAllVariables<T>() where T : Variable;
        T? GetVariableById<T>(Guid id) where T : Variable;
        void UpdateVariable(Variable variable);
        
    }

}
