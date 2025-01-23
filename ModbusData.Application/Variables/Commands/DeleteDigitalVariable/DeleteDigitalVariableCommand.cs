using ModbusData.Application.Abstract;
using System;

namespace ModbusData.Variables.Commands.DeleteDigitalVariable
{
    public record DeleteDigitalVariableCommand(Guid Id) : ICommand<bool>;
}
