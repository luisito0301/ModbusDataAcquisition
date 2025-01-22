using System;
using MediatR;
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario

namespace ModbusData.Application.Variables.Queries.GetAllAnalogicVariable
{
    public abstract record GetVariableByIdQuery(Guid Id) : IRequest<Variable>;

    public record GetAnalogicVariableByIdQuery(Guid Id) : GetVariableByIdQuery(Id);

    public record GetDigitalVariableByIdQuery(Guid Id) : GetVariableByIdQuery(Id);
}
