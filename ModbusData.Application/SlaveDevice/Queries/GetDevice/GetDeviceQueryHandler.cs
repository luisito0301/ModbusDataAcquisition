using MediatR;
using ModbusData.Contract.Devices;
using ModbusData.Domain.Entities.Device;
using ModbusData.Application.SlaveDevice.Queries.GetDevice;

namespace ModbusData.Application.Unit.Queries.GetUnit
{
    public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, Domain.Entities.Device.SlaveDevice>
    {
        private readonly IDeviceRepository<Domain.Entities.Device.SlaveDevice> _deviceRepository;

        public GetDeviceByIdQueryHandler(IDeviceRepository<Domain.Entities.Device.SlaveDevice> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<Domain.Entities.Device.SlaveDevice> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the unit by ID
            var device = _deviceRepository.GetById(request.Id); // Assuming you have a method to get by ID

            return Task.FromResult(device); // Return the found unit or null
        }
    }
}