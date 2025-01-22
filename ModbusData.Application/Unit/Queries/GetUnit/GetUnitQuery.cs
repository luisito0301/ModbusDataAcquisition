using System;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Variables.Queries.GetUnit
{
    public record GetUnitByIdQuery(Guid Id) : IRequest<Domain.Entities.Unit.Unit>;
}
