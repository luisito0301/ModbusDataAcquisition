using MediatR;
using ModbusData.Contract.Units;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Application.Unit.Queries.GetUnit;

namespace ModbusData.Application.Unit.Queries.GetUnit
{
    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, Domain.Entities.Unit.Unit>
    {
        private readonly IUnitRepository<Domain.Entities.Unit.Unit> _unitRepository;

        public GetUnitByIdQueryHandler(IUnitRepository<Domain.Entities.Unit.Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Task<Domain.Entities.Unit.Unit> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the unit by ID
            var unit = _unitRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(unit); // Return the found unit or null
        }
    }
}
