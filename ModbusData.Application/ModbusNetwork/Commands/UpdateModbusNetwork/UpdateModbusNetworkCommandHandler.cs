using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetwork;
using ModbusData.Contract;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Variables;


namespace ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetworks
{
    public class UpdateModbusNetworkCommandHandler : ICommandHandler<UpdateModbusNetworkCommand, bool>
    {
        private readonly IModbusNetworkRepository<Domain.Entities.Modbus_Network.ModbusNetwork> _modbusNetworkRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public UpdateModbusNetworkCommandHandler(
            IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> modbusNetworkRepository,
            IUnitOfWork unitOfWork)
        {
            _modbusNetworkRepository = modbusNetworkRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateModbusNetworkCommand request, CancellationToken cancellationToken)
        {
            // Find the existing ModbusNetwork
            var existingModbusNetwork = _modbusNetworkRepository.GetById(request.Id);

            if (existingModbusNetwork == null)
            {
                return Task.FromResult(false); // Return false if the Unit was not found
            }

            // Create a new instance of ModbusNetwork

            var updatedModbusNetwork = new ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork(
                existingModbusNetwork.Id, // keep the ID
                request.MasterIpAddress,
                request.Slaves
            );

            // Add the new ModbusNetwork to the repository
            _modbusNetworkRepository.Add(updatedModbusNetwork);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}