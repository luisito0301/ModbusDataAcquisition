using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Device; // Asegúrate de que esta referencia sea correcta
using System;
using System.Collections.Generic;
using ModbusData.Domain.ValueObjects;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;

namespace ModbusData.Application.SlaveDevice.Commands.CreateSlaveDevice
{
    public record CreateDeviceCommand(
        IP IpAddress, // Dirección IP del dispositivo
        List<Variable> variables // Lista de dispositivos esclavos asociados a la red
    ) : ICommand<Domain.Entities.Device.SlaveDevice>; // Asegúrate de que ModbusNetwork sea el tipo de retorno correcto
}