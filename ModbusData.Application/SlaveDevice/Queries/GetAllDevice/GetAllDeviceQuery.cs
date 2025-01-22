using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.SlaveDevice.Queries.GetAllDevice
{
    public record GetAllDeviceQuery : IRequest<List<ModbusData.Domain.Entities.Device.SlaveDevice>>;
}
