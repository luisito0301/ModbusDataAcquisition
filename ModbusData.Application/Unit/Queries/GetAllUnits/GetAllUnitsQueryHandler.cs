using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModbusData.Application.Abstract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Contract.Units;
using ModbusData.Application.Variables.Queries.GetAllUnit;

namespace ModbusData.Application.Variables.Queries.GetAllAnalogicVariable
{
    public class GetAllUnitQueryHandler : IRequestHandler<GetAllUnitQuery, List<ModbusData.Domain.Entities.Unit.Unit>>
    {
        private readonly IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> _unitRepository;

        public GetAllUnitQueryHandler(IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Task<List<ModbusData.Domain.Entities.Unit.Unit>> Handle(GetAllUnitQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all Units from the repository
            var unit = _unitRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(unit.ToList()); // Return the list of Units
        }
    }
}