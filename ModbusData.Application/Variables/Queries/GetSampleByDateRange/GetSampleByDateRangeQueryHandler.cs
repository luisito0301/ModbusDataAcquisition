using MediatR;
using ModbusData.Domain.Entities; // Ajustar el espacio de nombres si es necesario
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Domain.Records;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Queries.GetSampleByDateRange
{
    public class GetSamplesByDateRangeQueryHandler : IRequestHandler<GetSamplesByDateRangeQuery, List<Sample>>
    {
        private readonly IVariableRepository<Variable> _variableRepository;

        public GetSamplesByDateRangeQueryHandler(IVariableRepository<Variable> variableRepository)
        {
            _variableRepository = variableRepository;
        }

        public Task<List<Sample>> Handle(GetSamplesByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var variable = _variableRepository.GetById(request.VariableId);
            if (variable == null) return Task.FromResult(new List<Sample>());

            var samples = variable.Samples
                .Where(sample => sample.Date >= request.StartDate && sample.Date <= request.EndDate)
                .ToList();

            return Task.FromResult(samples);
        }
    }
}
