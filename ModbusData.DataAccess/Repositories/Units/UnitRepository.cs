using ModbusData.Domain.Entities.Unit;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Units;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.DataAccess.Repositories.Units
{
    public class UnitRepository<T> : RepositoryBase<T>, IUnitRepository<T> where T : Unit
    {
        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public UnitRepository(ApplicationContext context) : base(context)
        {
        }

    }

    
    
}
