using System;
using MediatR;
using ModbusData.Domain.Entities.Device; // Adjust the namespace as necessary

namespace ModbusData.Application.SlaveDevice.Queries.GetDevice
{
    public record GetDeviceByIdQuery(Guid Id) : IRequest<Domain.Entities.Device.SlaveDevice>;
}
