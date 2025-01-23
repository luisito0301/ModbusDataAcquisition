using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.Unit.Commands.UpdateUnit
{
    public record UpdateUnitCommand(
            Guid Id,
            string ManufactererName,  ///Nombre del fabricante
            string Code,  ///Codigo asociado a la unidad
            string AreaName, ///Nombre del area donde se encuentran
            List<Variable> _variables,
            UnitType Type) : ICommand<bool>;
}
