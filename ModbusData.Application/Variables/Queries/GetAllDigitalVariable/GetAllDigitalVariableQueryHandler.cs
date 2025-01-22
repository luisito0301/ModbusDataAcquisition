using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModbusData.Application.Abstract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Application.Variables.Queries.GetAllDigitalVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Queries.GetAllDigitalVariable
{
    public class GetAllDigitalVariableQueryHandler : IRequestHandler<GetAllDigitalVariableQuery, List<DigitalVariable>>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;

        public GetAllDigitalVariableQueryHandler(IVariableRepository<DigitalVariable> digitalVariableRepository)
        {
            _digitalVariableRepository = digitalVariableRepository;
        }

        public Task<List<DigitalVariable>> Handle(GetAllDigitalVariableQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all AnalogicVariables from the repository
            var digitalVariables = _digitalVariableRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(digitalVariables.ToList()); // Return the list of AnalogicVariables
        }
    }
}
