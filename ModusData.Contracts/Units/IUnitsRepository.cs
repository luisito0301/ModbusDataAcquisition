using ModbusData.Domain.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.Units
{
    public interface IUnitRepository
    {
        void AddUnit(Unit unit);
        void DeleteUnit(Unit unit);
        IEnumerable<Unit> GetAllUnits();
        Unit? GetUnitById(Guid id);
        void UpdateUnit(Unit unit);
    }

}
