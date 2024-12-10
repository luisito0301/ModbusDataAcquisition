using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Variables;

namespace ModbusData.DataAccess.Repositories.Variables
{
    /// <summary>
    /// Implementación del repositorio <see cref="IVariableRepository"/>.
    /// </summary>
    public class VariableRepository : RepositoryBase, IVariableRepository
    {
        // <summary>
        /// Describe las funcionalidades necesarias
        /// para dar persistencia a Variables.
        /// </summary>
        public VariableRepository(ApplicationContext context) : base(context)
        {
        }
        /// <summary>
        /// Añade una variable al soporte de datos.
        /// </summary>
        /// <param name="variable">Variable a añadir.</param>
        public void AddVariable(Variable variable)
        {
            _context.Variables.Add(variable);
        }
        /// <summary>
        /// Elimina un variable del soporte de datos.
        /// </summary>
        /// <param name="variable">Variable a eliminar.</param>
        public void DeleteVariable(Variable variable)
        {
            _context.Variables.Remove(variable);
        }
        /// <summary>
        /// Obtiene todas las variables del soporte de datos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllVariables<T>() where T : Variable
        {
            return _context.Set<T>().ToList();
        }
                 public T? GetVariableById<T>(Guid id) where T : Variable
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Actualiza el valor de una variable en el soporte de datos.
        /// </summary>
        /// <param name="variable">Instancia con la información a actualizar de la variable.</param>
        public void UpdateVariable(Variable variable)
        {
            _context.Variables.Update(variable);
        }
    }
}
