using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.SlaveDevice.Commands.CreateSlaveDevice;
using ModbusData.Contract;
using ModbusData.Contract.Devices;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.ValueObjects;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;

namespace ModbusData.Application.SlaveDevice.Commands.CreateDevice
{
    public class CreateDeviceCommandHandler : ICommandHandler<CreateDeviceCommand, ModbusData.Domain.Entities.Device.SlaveDevice>
    {
        private readonly IDeviceRepository<Domain.Entities.Device.SlaveDevice> _deviceRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeviceCommandHandler(
            IDeviceRepository<Domain.Entities.Device.SlaveDevice> deviceRepository,
            IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Domain.Entities.Device.SlaveDevice> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            // Create a new instance of Unit
            Domain.Entities.Device.SlaveDevice device = new Domain.Entities.Device.SlaveDevice(
                Guid.NewGuid(), // Generate a new ID
                request.IpAddress,
                request.variables
            );

            // Add the new Unit to the repository
            _deviceRepository.Add(device);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(device); // Return the created instance
        }
    }
}