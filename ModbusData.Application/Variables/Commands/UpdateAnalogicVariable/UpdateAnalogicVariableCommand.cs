using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.UpdateAnalogicVariable
{
    public record UpdateAnalogicVariableCommand(
        Guid Id,
        string Name,
        VariableType Type,
        bool IsMeasurement,
        string Code,
        TimeSpan SamplingPeriod,
        int ModbusAddress,
        double Value,
        Guid UnitId) : ICommand<bool>;
}