using System;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.ModbusNetwork.Queries.GetModbusNetwork
{
    public record GetModbusNetworkByIdQuery(Guid Id) : IRequest<Domain.Entities.Modbus_Network.ModbusNetwork>;
}
