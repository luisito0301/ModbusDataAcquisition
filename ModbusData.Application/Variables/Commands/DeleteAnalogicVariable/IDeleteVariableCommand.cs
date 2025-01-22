using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public interface IDeleteVariableCommand : ICommand<bool>
    {
        Guid VariableId { get; }
        VariableType Type { get; }
    }
}
