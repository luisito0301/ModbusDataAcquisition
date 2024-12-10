using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Variables;

namespace ModbusData.DataAccess.Repositories.Variables
{
    public class VariableRepository : RepositoryBase, IVariableRepository
    {
        public VariableRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddVariable(Variable variable)
        {
            _context.Set<Variable>().Add(variable);
        }

        public void DeleteVariable(Variable variable)
        {
            _context.Set<Variable>().Remove(variable);
        }

        public IEnumerable<T> GetAllVariables<T>() where T : Variable
        {
            return _context.Set<T>().ToList();
        }
                 public T? GetVariableById<T>(Guid id) where T : Variable
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateVariable(Variable variable)
        {
            _context.Set<Variable>().Update(variable);
        }
    }
}
