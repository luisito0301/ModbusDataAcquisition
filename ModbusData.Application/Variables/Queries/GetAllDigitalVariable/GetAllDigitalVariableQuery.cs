using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Variables.Queries.GetAllDigitalVariable
{
    public record GetAllDigitalVariableQuery : IRequest<List<DigitalVariable>>;
}