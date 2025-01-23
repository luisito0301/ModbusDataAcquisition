using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.ModbusNetwork.Queries.GetModbusNetwork
{
    public record GetModbusNetworkByIdQuery(Guid Id) : IRequest<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>;
}
