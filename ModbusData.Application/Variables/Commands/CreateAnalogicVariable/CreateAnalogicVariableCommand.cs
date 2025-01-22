using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;
namespace ModbusData.Application.Variables.Commands.CreateAnalogicVariable
{
        public record CreateAnalogicVariableCommand(
            string Name,
            VariableType Type,
            bool IsMeasurement,
            string Code,
            TimeSpan SamplingPeriod,
            int ModbusAddress,
            double Value,
           Guid UnitId) : ICommand<AnalogicVariable>;
    }

