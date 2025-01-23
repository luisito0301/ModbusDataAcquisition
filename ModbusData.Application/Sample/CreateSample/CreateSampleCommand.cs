using ModbusData.Application.Abstract;
using ModbusData.Domain.Records;
using ModbusData.Domain.Types;
using System;

namespace ModbusData.Application.Commands.CreateSample
{
    public record CreateSampleCommand(
        string VariableId,
        string Date,
        double Value) : ICommand<Domain.Records.Sample>;
        }


