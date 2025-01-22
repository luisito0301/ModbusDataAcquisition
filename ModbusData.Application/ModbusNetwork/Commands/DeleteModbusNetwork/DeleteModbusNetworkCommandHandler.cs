using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Device; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.ModbusNetwork.Commands.DeleteModbusNetwork;
using ModbusData.Contract.Devices;
using ModbusData.Contract.ModbusNetworks;


namespace ModbusData.Application.ModbusNetwork.Commands.DeleteModbusNetwork
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
            // Find the unit by id
            var modbusNetworkToDelete = _modbusNetworkRepository.GetById(request.id); // Assuming you have a method to get by ID

            if (modbusNetworkToDelete == null)
            {
                return Task.FromResult(false); // Return false if the unit was not found
            }

            // Delete the ModbusNetwork
            _modbusNetworkRepository.Delete(request.id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}
