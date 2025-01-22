using System;
using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Entities;
using ModbusData.Domain.Records;

namespace ModbusData.Application.Variables.Queries.GetSampleByDateRange
{
    public record GetSamplesByDateRangeQuery(Guid VariableId, DateTime StartDate, DateTime EndDate) : IRequest<List<Sample>>;
}
