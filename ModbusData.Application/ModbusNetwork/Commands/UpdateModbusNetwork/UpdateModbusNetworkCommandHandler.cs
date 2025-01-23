using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetwork;
using ModbusData.Contract;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetworks
{
    public class UpdateModbusNetworkCommandHandler : ICommandHandler<UpdateModbusNetworkCommand, bool>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _modbusNetworkRepository; // Asegúrate de tener un repositorio para ModbusNetwork
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
                return Task.FromResult(false); // Return false if the ModbusNetwork was not found
            }

            // Update the existing ModbusNetwork with new values
            existingModbusNetwork.MasterIpAddress = request.MasterIpAddress;
            existingModbusNetwork.Slaves = request.Slaves;

            // Update the ModbusNetwork in the repository
            _modbusNetworkRepository.Update(existingModbusNetwork);
            _unitOfWork.SaveChanges(); // Save changes

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}
