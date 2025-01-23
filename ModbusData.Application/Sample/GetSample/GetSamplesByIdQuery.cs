using MediatR;
using ModbusData.Domain.Records;
using System;
using System.Collections.Generic;

namespace ModbusData.Application.Sample.GetSample
{
    public class GetSamplesByVariableIdQuery : IRequest<IEnumerable<ModbusData.Domain.Records.Sample>>
    {
        public Guid Id { get; set; }

        public GetSamplesByVariableIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
