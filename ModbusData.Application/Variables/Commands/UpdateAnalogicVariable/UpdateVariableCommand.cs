using ModbusData.Application.Abstract;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.UpdateAnalogicVariable
{
    public abstract record UpdateVariableCommand(
        Guid VariableId,
        string Name,
        VariableType Type,
        bool IsMeasurement,
        string Code,
        TimeSpan SamplingPeriod,
        int ModbusAddress,
        Guid UnitId) : IUpdateVariableCommand;
}
