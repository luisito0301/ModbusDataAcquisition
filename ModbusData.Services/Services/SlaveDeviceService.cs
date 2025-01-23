using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using ModbusData.GrpcProtos;
using ModbusData.Application.SlaveDevice.Commands.CreateSlaveDevice;
using ModbusData.Application.SlaveDevice.Commands.DeleteSlaveDevice;
using ModbusData.Application.SlaveDevice.Commands.UpdateSlaveDevice;
using ModbusData.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using ModbusData.Application.SlaveDevice.Queries.GetDevice;
using ModbusData.Application.SlaveDevice.Queries.GetAllDevice;
using ModbusData.Application.SlaveDevice.Commands.UpdateDevice;
using ModbusData.SlaveDevice.Commands.DeleteDevice;

namespace ModbusData.Services.Services
{
    public class SlaveDeviceService : GrpcProtos.SlaveDeviceService.SlaveDeviceServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<SlaveDeviceService> _logger;

        public SlaveDeviceService(IMediator mediator, IMapper mapper, ILogger<SlaveDeviceService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<SlaveDeviceDTO> CreateSlaveDevice(CreateSlaveDeviceRequest request, ServerCallContext context)
        {
            var command = new CreateDeviceCommand(
                request.IpAddress.ToString().ToIp(), // Utilizamos el método de extensión ToIp
                request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList()
            );

            var result = _mediator.Send(command).Result; // Llamada síncrona
            return Task.FromResult(_mapper.Map<SlaveDeviceDTO>(result));
        }

        public override Task<NullableSlaveDeviceDTO> GetSlaveDevice(GetRequest request, ServerCallContext context)
        {
            var query = new GetDeviceByIdQuery(Guid.Parse(request.Id));
            var result = _mediator.Send(query).Result; // Llamada síncrona

            if (result is null)
            {
                _logger.LogWarning("SlaveDevice not found for ID: {SlaveDeviceId}", request.Id);
                return Task.FromResult(new NullableSlaveDeviceDTO { Null = NullValue.NullValue });
            }

            _logger.LogInformation("SlaveDevice found for ID: {SlaveDeviceId}", request.Id);
            return Task.FromResult(new NullableSlaveDeviceDTO { SlaveDevice = _mapper.Map<SlaveDeviceDTO>(result) });
        }

        public override Task<SlaveDevices> GetAllSlaveDevices(Empty request, ServerCallContext context)
        {
            var query = new GetAllDeviceQuery();
            var result = _mediator.Send(query).Result; // Llamada síncrona

            var slaveDeviceDTOs = new SlaveDevices();
            slaveDeviceDTOs.Items.AddRange(result.Select(m => _mapper.Map<SlaveDeviceDTO>(m)));

            return Task.FromResult(slaveDeviceDTOs);
        }

        public override Task<Empty> UpdateSlaveDevice(SlaveDeviceDTO request, ServerCallContext context)
        {
            var command = new UpdateDeviceCommand(
                Guid.Parse(request.Id),
                request.IpAddress.ToString().ToIp(), // Utilizamos el método de extensión ToIp
                request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList()
            );

            _mediator.Send(command).Wait(); // Llamada síncrona
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSlaveDevice(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteDeviceCommand(Guid.Parse(request.Id));
            _mediator.Send(command).Wait(); // Llamada síncrona
            return Task.FromResult(new Empty());
        }
    }

    
}
