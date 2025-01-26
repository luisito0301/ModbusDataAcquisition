using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;
namespace ModbusData.Application.Variables.Commands.CreateDigitalVariable
{
    public record CreateDigitalVariableCommand(
        string Name,
        VariableType Type,
        bool IsMeasurement,
        string Code,
        TimeSpan SamplingPeriod,
        int ModbusAddress,
        short Value,
       Guid UnitId) : ICommand<DigitalVariable>;
}
