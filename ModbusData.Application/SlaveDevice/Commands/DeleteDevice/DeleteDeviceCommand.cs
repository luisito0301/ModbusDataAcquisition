using ModbusData.Application.Abstract;

namespace ModbusData.SlaveDevice.Commands.DeleteDevice
{
    public record DeleteDeviceCommand(Guid id) : ICommand<bool>;
}
