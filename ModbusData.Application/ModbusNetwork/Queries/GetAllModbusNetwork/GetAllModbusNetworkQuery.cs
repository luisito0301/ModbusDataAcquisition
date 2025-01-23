using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.ModbusNetwork.Queries.GetAllModbusNetwork
{
    public record GetAllModbusNetworkQuery : IRequest<List<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>>;
}
