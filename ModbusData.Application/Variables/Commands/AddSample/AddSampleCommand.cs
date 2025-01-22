using System;
using MediatR;

namespace ModbusData.Application.Variables.Commands.AddSample
{
    public record AddSampleCommand(Guid VariableId, DateTime Date, double Value) : IRequest<bool>;
}
