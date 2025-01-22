using ModbusData.Application.Abstract;

namespace ModbusData.Units.Commands.DeleteUnits
{
    public record DeleteUnitCommand(Guid id) : ICommand<bool>;
}