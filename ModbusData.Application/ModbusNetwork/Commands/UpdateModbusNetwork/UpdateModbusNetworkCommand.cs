using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Device; // Asegúrate de que esta referencia sea correcta
using System;
using System.Collections.Generic;
using ModbusData.Domain.ValueObjects;

namespace ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetwork
{
    public record UpdateModbusNetworkCommand(
        Guid Id,
        IP MasterIpAddress, // Dirección IP del dispositivo maestro
        List<ModbusData.Domain.Entities.Device.SlaveDevice> Slaves // Lista de dispositivos esclavos asociados a la red
    ) : ICommand<bool>;
}