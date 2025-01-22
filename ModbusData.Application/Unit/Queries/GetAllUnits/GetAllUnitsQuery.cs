using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Unit.Queries.GetAllUnit
{
    public record GetAllUnitQuery : IRequest<List<ModbusData.Domain.Entities.Unit.Unit>>;
}