using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public record DeleteDigitalVariableCommand(Guid VariableId) : ICommand<bool>;
}
