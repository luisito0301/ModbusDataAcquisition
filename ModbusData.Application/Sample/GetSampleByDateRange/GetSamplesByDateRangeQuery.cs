using MediatR;
using ModbusData.Domain.Records;
using System;
using System.Collections.Generic;

namespace ModbusData.Application.Sample.GetSample
{
    public class GetSamplesByDateRangeQuery : IRequest<IEnumerable<ModbusData.Domain.Records.Sample>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetSamplesByDateRangeQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
