using MediatR;
using ModbusData.Domain.Records;
using ModbusData.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Sample.GetSample;

namespace ModbusData.Application.Sample.GetSamplesbyId
{
    public class GetSamplesByVariableIdQueryHandler : IRequestHandler<GetSamplesByVariableIdQuery, IEnumerable<ModbusData.Domain.Records.Sample>>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetSamplesByVariableIdQueryHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public Task<IEnumerable<ModbusData.Domain.Records.Sample>> Handle(GetSamplesByVariableIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the samples by variable ID
            var samples = _sampleRepository.GetSamplesByVariableId(request.Id); // Assuming you have a method to get by variable ID

            return Task.FromResult(samples); // Return the found samples
        }
    }
}
