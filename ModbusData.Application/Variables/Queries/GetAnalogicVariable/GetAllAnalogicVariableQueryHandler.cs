using MediatR;
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Contract.Variables;
using ModbusData.Application.Variables.Queries.GetAnalogicVariable;

namespace ModbusData.Application.Variables.Queries.GetAnalogicVariable
{
    public class GetAllVariablesQueryHandler : IRequestHandler<GetAllVariablesQuery, List<Variable>>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;

        public GetAllVariablesQueryHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IVariableRepository<DigitalVariable> digitalVariableRepository)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _digitalVariableRepository = digitalVariableRepository;
        }

        public Task<List<Variable>> Handle(GetAllVariablesQuery request, CancellationToken cancellationToken)
        {
            var analogicVariables = _analogicVariableRepository.GetAll();
            var digitalVariables = _digitalVariableRepository.GetAll();

            var allVariables = new List<Variable>();
            allVariables.AddRange(analogicVariables);
            allVariables.AddRange(digitalVariables);

            return Task.FromResult(allVariables);
        }
    }
}
