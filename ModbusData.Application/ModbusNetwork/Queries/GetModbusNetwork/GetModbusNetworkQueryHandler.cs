using MediatR;
using ModbusData.Contract.Units;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Application.Unit.Queries.GetUnit;
using ModbusData.Contract.ModbusNetworks;

namespace ModbusData.Application.ModbusNetwork.Queries.GetModbusNetwork
{
    public class GetModbusNetworkByIdQueryHandler : IRequestHandler<GetModbusNetworkByIdQuery, Domain.Entities.Modbus_Network.ModbusNetwork>
    {
        private readonly IModbusNetworkRepository<Domain.Entities.Modbus_Network.ModbusNetwork> _unitRepository;

        public GetModbusNetworkByIdQueryHandler(IModbusNetworkRepository<Domain.Entities.Modbus_Network.ModbusNetwork> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Task<Domain.Entities.Modbus_Network.ModbusNetwork> Handle(GetModbusNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the unit by ID
            var unit = _unitRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(unit); // Return the found unit or null
        }
    }
}
