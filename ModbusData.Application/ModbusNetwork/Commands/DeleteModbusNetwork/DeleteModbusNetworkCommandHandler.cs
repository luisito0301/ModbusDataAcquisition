using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Unit; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Unit.Commands.DeleteUnit;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Device;
using ModbusData.DataAccess.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.ModbusNetwork.Commands.CreateModbusNetwork;
using ModbusData.ModbusNetwork.Commands.DeleteModbusNetwork;

namespace ModbusData.Application.ModubusNetwork.Commands.DeleteModubusNetwork
{
    public class DeleteModbusNetworkCommandHandler : ICommandHandler<DeleteModbusNetworkCommand, bool>
    {
        private readonly IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> _modbusNetworkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModbusNetworkCommandHandler(
            IModbusNetworkRepository<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork> modbusNetworkRepository,
            IUnitOfWork unitOfWork)
        {
            _modbusNetworkRepository = modbusNetworkRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteModbusNetworkCommand request, CancellationToken cancellationToken)
        {
            // Find the ModubusNetwork by id
            var modbusNetworkToDelete = _modbusNetworkRepository.GetById(request.id); // Assuming you have a method to get by ID

            if (modbusNetworkToDelete == null)
            {
                return Task.FromResult(false); // Return false if the ModubusNetwork was not found
            }

            // Delete the AnalogicVariable
            _modbusNetworkRepository.Delete(request.id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}
