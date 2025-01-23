using MediatR;
using ModbusData.Contract.Variables;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Application.Variables.Queries.GetDigitalVariable;

namespace ModbusData.Application.Variables.Queries.GetDigitalVariable
{
    public class GetDigitalVariableByIdQueryHandler : IRequestHandler<GetDigitalVariableByIdQuery, DigitalVariable>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;

        public GetDigitalVariableByIdQueryHandler(IVariableRepository<DigitalVariable> digitalVariableRepository)
        {
            _digitalVariableRepository = digitalVariableRepository;
        }

        public Task<DigitalVariable> Handle(GetDigitalVariableByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the digitalVariable by ID
            var digitalVariable = _digitalVariableRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(digitalVariable); // Return the found digitalVariable or null
        }
    }
}
