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
using ModbusData.SlaveDevice.Commands.DeleteDevice;
using ModbusData.Application.SlaveDevice.Commands.UpdateDevice;

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

        public override async Task<SlaveDeviceDTO> CreateSlaveDevice(CreateSlaveDeviceRequest request, ServerCallContext context)
        {
            try
            {
                var command = new CreateDeviceCommand(
                    IP.Parse(request.IpAddress), // Utilizamos el método Parse
                    request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList()
                );

                var result = await _mediator.Send(command);
                return _mapper.Map<SlaveDeviceDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating slave device");
                throw new RpcException(new Status(StatusCode.Unknown, "An error occurred while creating the slave device"));
            }
        }

        public override async Task<NullableSlaveDeviceDTO> GetSlaveDevice(GetRequest request, ServerCallContext context)
        {
            try
            {
                var query = new GetDeviceByIdQuery(Guid.Parse(request.Id));
                var result = await _mediator.Send(query);

                if (result is null)
                {
                    _logger.LogWarning("SlaveDevice not found for ID: {SlaveDeviceId}", request.Id);
                    return new NullableSlaveDeviceDTO { Null = NullValue.NullValue };
                }

                _logger.LogInformation("SlaveDevice found for ID: {SlaveDeviceId}", request.Id);
                return new NullableSlaveDeviceDTO { SlaveDevice = _mapper.Map<SlaveDeviceDTO>(result) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting slave device");
                throw new RpcException(new Status(StatusCode.Unknown, "An error occurred while getting the slave device"));
            }
        }

        public override async Task<SlaveDevices> GetAllSlaveDevices(Empty request, ServerCallContext context)
        {
            try
            {
                var query = new GetAllDeviceQuery();
                var result = await _mediator.Send(query);

                var slaveDeviceDTOs = new SlaveDevices();
                slaveDeviceDTOs.Items.AddRange(result.Select(m => _mapper.Map<SlaveDeviceDTO>(m)));

                return slaveDeviceDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all slave devices");
                throw new RpcException(new Status(StatusCode.Unknown, "An error occurred while getting all slave devices"));
            }
        }

        public override async Task<Empty> UpdateSlaveDevice(SlaveDeviceDTO request, ServerCallContext context)
        {
            try
            {
                var command = new UpdateDeviceCommand(
                    Guid.Parse(request.Id),
                    IP.Parse(request.IpAddress), // Utilizamos el método Parse
                    request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList()
                );

                await _mediator.Send(command);
                return new Empty();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating slave device");
                throw new RpcException(new Status(StatusCode.Unknown, "An error occurred while updating the slave device"));
            }
        }

        public override async Task<Empty> DeleteSlaveDevice(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                var command = new DeleteDeviceCommand(Guid.Parse(request.Id));
                await _mediator.Send(command);
                return new Empty();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting slave device");
                throw new RpcException(new Status(StatusCode.Unknown, "An error occurred while deleting the slave device"));
            }
        }
    }
}
