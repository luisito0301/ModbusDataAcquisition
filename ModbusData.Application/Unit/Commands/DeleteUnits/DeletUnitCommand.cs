using ModbusData.Application.Abstract;

namespace ModbusData.Unit.Commands.DeleteUnit
{
    public record DeleteUnitCommand(Guid id) : ICommand<bool>;
}