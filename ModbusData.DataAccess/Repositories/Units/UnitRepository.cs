using ModbusData.Domain.Entities.Unit;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Units;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.DataAccess.Repositories.Units
{
    public class UnitRepository : RepositoryBase, IUnitRepository
    {
        public UnitRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddUnit(Unit unit)
        {
            // Check if the AnalogicVariable instances are already being tracked
            foreach (var variable in unit.Variables.ToList()) // Use ToList() to avoid modifying the collection while iterating
            {
                var existingVariable = _context.Set<AnalogicVariable>().Local.FirstOrDefault(v => v.Id == variable.Id);
                if (existingVariable != null)
                {
                    // If the variable is already tracked, add it to the unit's Variables list if not already present
                    if (!unit.Variables.Contains(existingVariable))
                    {
                        unit.AddVariable(existingVariable);
                    }
                }
                else
                {
                    // If not tracked, add the new variable
                    unit.AddVariable(variable);
                }
            }

            // Now add the unit to the context
            _context.Set<Unit>().Add(unit);
        }

        public void UpdateUnit(Unit unit)
        {
            // Check if the unit is already being tracked
            var existingUnit = _context.Set<Unit>().Local.FirstOrDefault(u => u.Id == unit.Id);
            if (existingUnit != null)
            {
                // Update the existing tracked entity
                _context.Entry(existingUnit).CurrentValues.SetValues(unit);
            }
            else
            {
                _context.Set<Unit>().Update(unit);
            }
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

     
    }
}
