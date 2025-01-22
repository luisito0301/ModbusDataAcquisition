using ModbusData.Application.Abstract;
using ModbusData.Application.Variables.Commands.CreateAnalogicVariable;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.CreateAnalogicVariable
{
    public abstract record CreateVariableCommand(
        string Name,
        VariableType Type,
        bool IsMeasurement,
        string Code,
        TimeSpan SamplingPeriod,
        int ModbusAddress,
        Guid UnitId) : ICreateVariableCommand;
}
