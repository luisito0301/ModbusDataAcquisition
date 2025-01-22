using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Variables.Commands.CreateAnalogicVariable
{
    public interface ICreateVariableCommand : ICommand<Variable>
    {
        string Name { get; }
        VariableType Type { get; }
        bool IsMeasurement { get; }
        string Code { get; }
        TimeSpan SamplingPeriod { get; }
        int ModbusAddress { get; }
        Guid UnitId { get; }
    }
}
