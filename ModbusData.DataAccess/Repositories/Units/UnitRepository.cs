using ModbusData.Domain.Entities.Unit;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Units;

namespace ModbusData.DataAccess.Repositories.Units
{
    public class UnitRepository : RepositoryBase, IUnitRepository
    {
        public UnitRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddUnit(Unit unit)
        {
            _context.Set<Unit>().Add(unit);
        }

        public void DeleteUnit(Unit unit)
        {
            _context.Set<Unit>().Remove(unit);
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return _context.Set<Unit>().ToList();
        }

        public Unit? GetUnitById(Guid id)
        {
            return _context.Set<Unit>().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateUnit(Unit unit)
        {
            _context.Set<Unit>().Update(unit);
        }
    }
}
