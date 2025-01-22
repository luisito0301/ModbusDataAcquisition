using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.Unit.Commands.UpdateModbusNetwork
{
    public record UpdateModbusNetworkCommand(
        Guid Id,
            IP MasterIpAddress, // Dirección IP del dispositivo maestro
        List<SlaveDevice> Slaves // Lista de dispositivos esclavos asociados a la red
    ) : ICommand<bool>;
}
