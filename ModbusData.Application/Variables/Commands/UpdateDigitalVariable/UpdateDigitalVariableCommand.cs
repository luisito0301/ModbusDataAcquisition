using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.UpdateDigitalVariable
{
    public record UpdateDigitalVariableCommand(
        Guid Id,
        string Name,
        VariableType Type,
        bool IsMeasurement,
        string Code,
        TimeSpan SamplingPeriod,
        int ModbusAddress,
        short Value,
        Guid UnitId) : ICommand<bool>;
}