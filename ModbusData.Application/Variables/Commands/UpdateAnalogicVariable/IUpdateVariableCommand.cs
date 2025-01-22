using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.UpdateAnalogicVariable
{
    public interface IUpdateVariableCommand : ICommand<bool>
    {
        Guid VariableId { get; }
        string Name { get; }
        VariableType Type { get; }
        bool IsMeasurement { get; }
        string Code { get; }
        TimeSpan SamplingPeriod { get; }
        int ModbusAddress { get; }
        Guid UnitId { get; }
    }
}
