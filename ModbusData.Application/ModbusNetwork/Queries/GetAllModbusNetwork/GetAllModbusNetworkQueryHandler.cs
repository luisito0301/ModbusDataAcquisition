using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModbusData.Application.Abstract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Contract.Devices;
using ModbusData.Application.SlaveDevice.Queries.GetAllDevice;
using ModbusData.Application.ModbusNetwork.Queries.GetAllModbusNetwork;
using ModbusData.Contract.ModbusNetworks;

namespace ModbusData.Application.Unit.Queries.GetAllUnits
{
    public class GetAllModbusNetworkQueryHandler : IRequestHandler<GetAllModbusNetworkQuery, List<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _modbusRepository;

        public GetAllModbusNetworkQueryHandler(IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> modbusRepository)
        {
            _modbusRepository = modbusRepository;
        }

        public Task<List<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>> Handle(GetAllModbusNetworkQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all Units from the repository
            var device = _modbusRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(device.ToList()); // Return the list of Units
        }
    }
}