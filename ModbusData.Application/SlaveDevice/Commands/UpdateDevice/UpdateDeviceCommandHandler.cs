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
        private readonly IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> _deviceRepository; // Asegúrate de tener un repositorio para SlaveDevice
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
            // Find the existing SlaveDevice
            var existingDevice = _deviceRepository.GetById(request.Id);

            if (existingDevice == null)
            {
                return Task.FromResult(false); // Return false if the SlaveDevice was not found
            }

            // Update the existing SlaveDevice with new values
            existingDevice.IpAddress = request.IpAddress;
            existingDevice.Variables = request.variables;

            // Update the SlaveDevice in the repository
            _deviceRepository.Update(existingDevice);
            _unitOfWork.SaveChanges(); // Save changes

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}
