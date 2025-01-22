using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public abstract record DeleteVariableCommand(Guid VariableId,VariableType Type) : IDeleteVariableCommand;
}
