using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Device; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.SlaveDevice.Commands.DeleteDevice;
using ModbusData.Contract.Devices;
using ModbusData.Contract.Units;


namespace ModbusData.Application.SlaveDevice.Commands.DeleteSlaveDevice
{
    public class DeleteDeviceCommandHandler : ICommandHandler<DeleteDeviceCommand, bool>
    {
        private readonly IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeviceCommandHandler(
            IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> deviceRepository,
            IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            // Find the unit by id
            var deviceToDelete = _deviceRepository.GetById(request.id); // Assuming you have a method to get by ID

            if (deviceToDelete == null)
            {
                return Task.FromResult(false); // Return false if the unit was not found
            }

            // Delete the AnalogicVariable
            _deviceRepository.Delete(request.id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}