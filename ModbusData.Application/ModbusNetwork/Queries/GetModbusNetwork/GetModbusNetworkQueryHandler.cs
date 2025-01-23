using MediatR;
using ModbusData.Contract.ModbusNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.ModbusNetwork.Queries.GetModbusNetwork
{
    public class GetModbusNetworkByIdQueryHandler : IRequestHandler<GetModbusNetworkByIdQuery, ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _deviceRepository;

        public GetModbusNetworkByIdQueryHandler(IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> Handle(GetModbusNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the unit by ID
            var device = _deviceRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(device); // Return the found unit or null
        }
    }
}
