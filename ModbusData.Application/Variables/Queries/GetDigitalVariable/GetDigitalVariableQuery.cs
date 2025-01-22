using System;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Variables.Queries.GetDigitalVariable
{
    public record GetDigitalVariableByIdQuery(Guid Id) : IRequest<DigitalVariable>;
}