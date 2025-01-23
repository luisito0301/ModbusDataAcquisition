using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Device; // Asegúrate de que esta referencia sea correcta
using System;
using System.Collections.Generic;
using ModbusData.Domain.ValueObjects;

namespace ModbusData.Application.ModbusNetwork.Commands.CreateModbusNetwork
{
    public record CreateModbusNetworkCommand(
        IP MasterIpAddress, // Dirección IP del dispositivo maestro
        List<Domain.Entities.Device.SlaveDevice> Slaves // Lista de dispositivos esclavos asociados a la red
    ) : ICommand<Domain.Entities.Modbus_Network.ModbusNetwork>; // Asegúrate de que ModbusNetwork sea el tipo de retorno correcto
}
