using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModbusData.Application.Abstract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Contract.Devices;
using ModbusData.Application.SlaveDevice.Queries.GetAllDevice;

namespace ModbusData.Application.Unit.Queries.GetAllUnits
{
    public class GetAllDeviceQueryHandler : IRequestHandler<GetAllDeviceQuery, List<ModbusData.Domain.Entities.Device.SlaveDevice>>
    {
        private readonly IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> _deviceRepository;

        public GetAllDeviceQueryHandler(IDeviceRepository<ModbusData.Domain.Entities.Device.SlaveDevice> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<List<ModbusData.Domain.Entities.Device.SlaveDevice>> Handle(GetAllDeviceQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all Units from the repository
            var device = _deviceRepository.GetAll(); // Assuming you have a method to get all entities

            return Task.FromResult(device.ToList()); // Return the list of Units
        }
    }
}