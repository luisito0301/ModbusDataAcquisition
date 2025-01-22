using MediatR;
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Queries.GetAllAnalogicVariable
{
    public class GetVariableByIdQueryHandler : IRequestHandler<GetVariableByIdQuery, Variable>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;

        public GetVariableByIdQueryHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IVariableRepository<DigitalVariable> digitalVariableRepository)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _digitalVariableRepository = digitalVariableRepository;
        }

        public Task<Variable> Handle(GetVariableByIdQuery request, CancellationToken cancellationToken)
        {
            Variable result;

            if (request is GetAnalogicVariableByIdQuery)
            {
                result = _analogicVariableRepository.GetById(request.Id);
            }
            else if (request is GetDigitalVariableByIdQuery)
            {
                result = _digitalVariableRepository.GetById(request.Id);
            }
            else
            {
                throw new ArgumentException("Unsupported query type.");
            }

            return Task.FromResult(result);
        }
    }
}
