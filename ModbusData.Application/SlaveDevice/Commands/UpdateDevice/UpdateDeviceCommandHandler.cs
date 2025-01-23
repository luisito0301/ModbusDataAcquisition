using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.SlaveDevice.Commands.UpdateDevice;
using ModbusData.Contract;
using ModbusData.Contract.Devices;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Variables;


namespace ModbusData.Application.SlaveDevice.Commands.UpdateSlaveDevice
{
    public class UpdateDeviceCommandHandler : ICommandHandler<UpdateDeviceCommand, bool>
    {
        private readonly IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> _deviceRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeviceCommandHandler(
            IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> deviceRepository,
            IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            // Find the existing Unit
            var existingDevice = _deviceRepository.GetById(request.Id);

            if (existingDevice == null)
            {
                return Task.FromResult(false); // Return false if the Unit was not found
            }

            // Create a new instance of Unit

            var updatedDevice = new ModbusData.Domain.Entities.Device.SlaveDevice(
                existingDevice.Id, // keep the ID
                request.IpAddress,
                request.variables

            );

            // Add the new Unit to the repository
            _deviceRepository.Add(updatedDevice);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}