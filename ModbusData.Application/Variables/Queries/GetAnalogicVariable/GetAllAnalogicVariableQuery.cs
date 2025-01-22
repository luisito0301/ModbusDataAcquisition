using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.Application.Variables.Queries.GetAnalogicVariable
{
    public record GetAllVariablesQuery : IRequest<List<Variable>>;
}
