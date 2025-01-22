using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModbusData.Application.Abstract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Application.Variables.Queries.GetAllAnalogicVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Queries.GetAllAnalogicVariable
{
    public class GetAllAnalogicVariableQueryHandler : IRequestHandler<GetAllAnalogicVariableQuery, List<AnalogicVariable>>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;

        public GetAllAnalogicVariableQueryHandler(IVariableRepository<AnalogicVariable> analogicVariableRepository)
        {
            _analogicVariableRepository = analogicVariableRepository;
        }

        public Task<List<AnalogicVariable>> Handle(GetAllAnalogicVariableQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all AnalogicVariables from the repository
            var analogicVariables = _analogicVariableRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(analogicVariables.ToList()); // Return the list of AnalogicVariables
        }
    }
}