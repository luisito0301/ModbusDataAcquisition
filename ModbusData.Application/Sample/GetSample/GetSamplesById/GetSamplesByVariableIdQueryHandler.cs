using MediatR;
using ModbusData.Domain.Records;
using ModbusData.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusData.Application.Sample.GetSamplebyId;

namespace ModbusData.Application.Sample.GetSamplesbyId
{
    public class GetSampleByIdQueryHandler : IRequestHandler<GetSampleByVariableIdQuery, Domain.Records.Sample>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetSampleByIdQueryHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public Task<IEnumerable<Domain.Records.Sample>> Handle(GetSampleByVariableIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the unit by ID
            var sample = _sampleRepository.GetSamplesByVariableId(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(sample); // Return the found samples
        }
    }
}