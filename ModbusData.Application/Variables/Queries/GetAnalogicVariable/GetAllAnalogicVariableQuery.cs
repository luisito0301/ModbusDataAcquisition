using System.Collections.Generic;
using MediatR;
using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.Application.Variables.Queries.GetAnalogicVariable
{
    public record GetAllVariablesQuery : ICommand<List<Variable>>;
}
