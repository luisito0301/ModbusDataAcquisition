using ModbusData.Application.Abstract;

namespace ModbusData.ModbusNetwork.Commands.DeleteModbusNetwork
{
    public record DeleteModbusNetworkCommand(Guid id) : ICommand<bool>;
}
