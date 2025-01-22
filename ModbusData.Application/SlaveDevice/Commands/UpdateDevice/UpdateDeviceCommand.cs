using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Device; // Asegúrate de que esta referencia sea correcta
using System;
using System.Collections.Generic;
using ModbusData.Domain.ValueObjects;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;

namespace ModbusData.Application.SlaveDevice.Commands.UpdateDevice
{
    public record UpdateDeviceCommand(
        Guid Id,
        IP IpAddress, // Dirección IP del dispositivo
        List<Variable> variables // Lista de variables asociados al dispositivo
    ) : ICommand<bool>; 
}
