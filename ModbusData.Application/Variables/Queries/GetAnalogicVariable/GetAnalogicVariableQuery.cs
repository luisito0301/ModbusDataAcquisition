using System;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Variables.Queries.GetAnalogicVariable
{
    public record GetAnalogicVariableByIdQuery(Guid Id) : IRequest<AnalogicVariable>;
}