using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.Unit.Commands.UpdateModbusNetwork;
using ModbusData.Application.Unit.Commands.UpdateUnit;
using ModbusData.Contract;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.Contract.Units;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Unit; // Asegúrate de que la ruta sea correcta
using ModbusData.Domain.Entities.Variables;


namespace ModbusData.Application.Units.Commands.UpdateModbusNetwork
{
    public class UpdateModbusNetworkCommandHandler : ICommandHandler<UpdateModbusNetworkCommand, bool>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _modbusRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public UpdateModbusNetworkCommandHandler(
            IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> modbusRepository,
            IUnitOfWork unitOfWork)
        {
            _modbusRepository = modbusRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateModbusNetworkCommand request, CancellationToken cancellationToken)
        {
            // Find the existing Unit
            var existingModbus = _modbusRepository.GetById(request.Id);

            if (existingModbus == null)
            {
                return Task.FromResult(false); // Return false if the Unit was not found
            }

            // Create a new instance of Unit
            var updatedModbus = new ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork(
                existingModbus.Id, // keep the ID
                request.MasterIpAddress,
                request.Slaves
                

            );

            // Add the new Unit to the repository
            _modbusRepository.Add(updatedModbus);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}

