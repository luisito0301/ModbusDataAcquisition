using ModbusData.Application.Abstract;
using System;

namespace ModbusData.Variables.Commands.DeleteAnalogicVariable
{
    public record DeleteAnalogicVariableCommand(Guid Id) : ICommand<bool>;
}