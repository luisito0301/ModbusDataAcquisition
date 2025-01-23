using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using ModbusData.GrpcProtos;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Application.ModbusNetwork.Commands.CreateModbusNetwork;
using ModbusData.Application.ModbusNetwork.Commands.DeleteModbusNetwork;
using ModbusData.Application.ModbusNetwork.Commands.UpdateModbusNetwork;
using ModbusData.Application.ModbusNetwork.Queries.GetModbusNetwork;
using ModbusData.Application.ModbusNetwork.Queries.GetAllModbusNetwork;
using ModbusData.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using ModbusData.ModbusNetwork.Commands.DeleteModbusNetwork;

namespace ModbusData.Services.Services
{
    public class ModbusNetworkService : GrpcProtos.ModbusNetworkService.ModbusNetworkServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ModbusNetworkService> _logger;

        public ModbusNetworkService(IMediator mediator, IMapper mapper, ILogger<ModbusNetworkService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<ModbusNetworkDTO> CreateModbusNetwork(CreateModbusNetworkRequest request, ServerCallContext context)
        {
            var command = new CreateModbusNetworkCommand(
                request.MasterIpAddress.ToString().ToIp(), // Utilizamos el método de extensión ToIp
                request.Slaves.Select(s => _mapper.Map<ModbusData.Domain.Entities.Device.SlaveDevice>(s)).ToList()
            );

            var result = _mediator.Send(command).Result; // Llamada síncrona
            return Task.FromResult(_mapper.Map<ModbusNetworkDTO>(result));
        }

        public override Task<NullableModbusNetworkDTO> GetModbusNetwork(GetRequest request, ServerCallContext context)
        {
            var query = new GetModbusNetworkByIdQuery(Guid.Parse(request.Id));
            var result = _mediator.Send(query).Result; // Llamada síncrona

            if (result is null)
            {
                _logger.LogWarning("ModbusNetwork not found for ID: {ModbusNetworkId}", request.Id);
                return Task.FromResult(new NullableModbusNetworkDTO { Null = NullValue.NullValue });
            }

            _logger.LogInformation("ModbusNetwork found for ID: {ModbusNetworkId}", request.Id);
            return Task.FromResult(new NullableModbusNetworkDTO { ModbusNetwork = _mapper.Map<ModbusNetworkDTO>(result) });
        }

        public override Task<ModbusNetworks> GetAllModbusNetworks(Empty request, ServerCallContext context)
        {
            var query = new GetAllModbusNetworkQuery();
            var result = _mediator.Send(query).Result; // Llamada síncrona

            var modbusNetworkDTOs = new ModbusNetworks();
            modbusNetworkDTOs.Items.AddRange(result.Select(m => _mapper.Map<ModbusNetworkDTO>(m)));

            return Task.FromResult(modbusNetworkDTOs);
        }

        public override Task<Empty> UpdateModbusNetwork(ModbusNetworkDTO request, ServerCallContext context)
        {
            var command = new UpdateModbusNetworkCommand(
                Guid.Parse(request.Id),
                request.MasterIpAddress.ToString().ToIp(), // Utilizamos el método de extensión ToIp
                request.Slaves.Select(s => _mapper.Map<ModbusData.Domain.Entities.Device.SlaveDevice>(s)).ToList()
            );

            _mediator.Send(command).Wait(); // Llamada síncrona
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteModbusNetwork(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteModbusNetworkCommand(Guid.Parse(request.Id));
            _mediator.Send(command).Wait(); // Llamada síncrona
            return Task.FromResult(new Empty());
        }
    }

    public static class IpExtensions
    {
        public static Domain.ValueObjects.IP ToIp(this string ipString)
        {
            return Domain.ValueObjects.IP.Parse(ipString);
        }
    }
}
