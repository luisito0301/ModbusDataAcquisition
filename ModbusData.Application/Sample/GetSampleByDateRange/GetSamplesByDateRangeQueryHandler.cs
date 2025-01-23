using MediatR;
using ModbusData.Domain.Records;
using ModbusData.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Sample.GetSample;

namespace ModbusData.Application.Sample.GetSamplesByDateRange
{
    public class GetSamplesByDateRangeQueryHandler : IRequestHandler<GetSamplesByDateRangeQuery, IEnumerable<ModbusData.Domain.Records.Sample>>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetSamplesByDateRangeQueryHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public Task<IEnumerable<ModbusData.Domain.Records.Sample>> Handle(GetSamplesByDateRangeQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the samples by date range
            var samples = _sampleRepository.GetSamplesByDateRange(request.StartDate, request.EndDate);

            return Task.FromResult(samples); // Return the found samples
        }
    }
}
