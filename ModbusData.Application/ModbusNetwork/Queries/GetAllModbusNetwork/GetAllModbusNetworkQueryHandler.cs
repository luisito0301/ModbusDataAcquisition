using MediatR;
using ModbusData.Contract.ModbusNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.ModbusNetwork.Queries.GetAllModbusNetwork
{
    public class GetAllModbusNetworkByIdQueryHandler : IRequestHandler<GetAllModbusNetworkByIdQuery, List<Domain.Entities.Modbus_Network.ModbusNetwork>>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _modbusRepository;

        public GetAllModbusNetworkByIdQueryHandler(IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> modbusRepository)
        {
            _modbusRepository = modbusRepository;
        }

        public Task<List<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>> Handle(GetAllModbusNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all Units from the repository
            var unit = _modbusRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(unit.ToList()); // Return the list of Units
        }
    }
}
