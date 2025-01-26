using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModbusData.Application.Abstract;

namespace ModbusData.ModbusNetwork.Commands.DeleteModbusNetwork
{
    public record DeleteModbusNetworkCommand(Guid id) : ICommand<bool>;
}