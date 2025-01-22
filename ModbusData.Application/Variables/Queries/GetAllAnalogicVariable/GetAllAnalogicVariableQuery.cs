using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary

namespace ModbusData.Application.Variables.Queries.GetAllAnalogicVariable
{
    public record GetAllAnalogicVariableQuery : IRequest<List<AnalogicVariable>>;
}