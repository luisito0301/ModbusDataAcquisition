using MediatR;
using ModbusData.Contract.Variables;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Application.Variables.Queries.GetAnalogicVariable;

namespace ModbusData.Application.Variables.Queries.GetAnalogicVariable
{
    public class GetAnalogicVariableByIdQueryHandler : IRequestHandler<GetAnalogicVariableByIdQuery, AnalogicVariable>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;

        public GetAnalogicVariableByIdQueryHandler(IVariableRepository<AnalogicVariable> analogicVariableRepository)
        {
            _analogicVariableRepository = analogicVariableRepository;
        }

        public Task<AnalogicVariable> Handle(GetAnalogicVariableByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the AnalogicVariable by ID
            var analogicVariable = _analogicVariableRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(analogicVariable); // Return the found AnalogicVariable or null
        }
    }
}