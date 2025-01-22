using MediatR;
using ModbusData.Application.Abstract;
using System;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public record DeleteAnalogicVariableCommand(Guid VariableId) : ICommand<bool>;
}
