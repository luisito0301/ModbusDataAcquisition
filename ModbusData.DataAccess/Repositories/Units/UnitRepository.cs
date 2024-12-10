using ModbusData.Domain.Entities.Unit;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Units;

namespace ModbusData.DataAccess.Repositories.Units
{
    /// <summary>
    /// Implementación del repositorio <see cref="IUnitRepository"/>.
    /// </summary>
    public class UnitRepository : RepositoryBase, IUnitRepository
    {
        /// <summary>
        /// Describe las funcionalidades necesarias
        /// para dar persistencia a unidades.
        /// </summary>
        public UnitRepository(ApplicationContext context) : base(context)
        {
        }
        /// <summary>
        /// Añade una unidad al soporte de datos.
        /// </summary>
        /// <param name="unit">Unidad a añadir.</param>
        public void AddUnit(Unit unit)
        {
            _context.Set<Unit>().Add(unit);
        }
        /// <summary>
        /// Elimina una unidad del soporte de datos.
        /// </summary>
        /// <param name="unit">Unidada eliminar.</param>
        public void DeleteUnit(Unit unit)
        {
            _context.Set<Unit>().Remove(unit);
        }
        /// <summary>
        /// Obtiene todas las unidades del soporte de datos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Unit> GetAllUnits()
        {
            return _context.Set<Unit>().ToList();
        }
        /// <summary>
        /// Obtiene una unidad del soporte de datos a partir de su identificador.
        /// </summary>
        /// <param name="id">Identificador de la unidad.</param>
        /// <returns>Unidad obtenida del soporte de datos; de no existir, <see langword="null"/>.</returns>
        public Unit? GetUnitById(Guid id)
        {
            return _context.Set<Unit>().FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Actualiza el valor de una unidad en el soporte de datos.
        /// </summary>
        /// <param name="unit">Instancia con la información a actualizar de la unidad.</param>
        public void UpdateUnit(Unit unit)
        {
            _context.Set<Unit>().Update(unit);
        }
    }
}
