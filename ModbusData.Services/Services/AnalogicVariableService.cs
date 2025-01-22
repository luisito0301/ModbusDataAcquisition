using AutoMapper;
using ModbusData.Application.Variables.Commands.CreateAnalogicVariable;
using ModbusData.Application.Variables.Commands.DeleteAnalogicVariable;
using ModbusData.Application.Variables.Commands.UpdateAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAllAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAnalogicVariable;
using ModbusData.Domain.Types; // Clases del dominio
using ModbusData.Domain.Entities.Variables; // Clases del dominio
using ModbusData.GrpcProtos; // Clases de gRPC
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ModbusData.Services.Services
{
    public class AnalogicVariableService : GrpcProtos.AnalogicVariable.AnalogicVariableBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AnalogicVariableService> _logger;

        public AnalogicVariableService(IMediator mediator, IMapper mapper, ILogger<AnalogicVariableService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<AnalogicVariableDTO> CreateAnalogicVariable(CreateAnalogicVariableRequest request, ServerCallContext context)
        {
            var command = new CreateAnalogicVariableCommand(
                request.Name,
                (Domain.Types.VariableType)request.Type, // VariableType del dominio
                request.IsMeasurement,
                request.Code,
                TimeSpan.Parse(request.SamplingPeriod),
                request.ModbusAddress,
                request.Value,
                Guid.Parse(request.Unitid)
            );

            var result = _mediator.Send(command).Result; // Llamada sincrónica
            return Task.FromResult(_mapper.Map<AnalogicVariableDTO>(result)); // AnalogicVariableDTO de gRPC
        }

        public override Task<NullableAnalogicVariableDTO> GetAnalogicVariable(GetRequest request, ServerCallContext context)
        {
            var query = new GetAnalogicVariableByIdQuery(Guid.Parse(request.Id));
            var result = _mediator.Send(query).Result; // Llamada sincrónica

            if (result == null)
            {
                _logger.LogWarning("AnalogicVariable not found for ID: {AnalogicVariableId}", request.Id);
                return Task.FromResult(new NullableAnalogicVariableDTO { Null = NullValue.NullValue });
            }

            _logger.LogInformation("AnalogicVariable found for ID: {AnalogicVariableId}", request.Id);
            return Task.FromResult(new NullableAnalogicVariableDTO { AnalogicVariable = _mapper.Map<AnalogicVariableDTO>(result) }); // AnalogicVariableDTO de gRPC
        }

        public override Task<GrpcProtos.AnalogicVariable> GetAllAnalogicVariables(Empty request, ServerCallContext context)
        {
            var query = new GetAllAnalogicVariableQuery();
            var result = _mediator.Send(query).Result; // Llamada sincrónica

            var analogicVariableDTOs = new AnalogicVariables();
            analogicVariableDTOs.Items.AddRange(result.Select(m => _mapper.Map<AnalogicVariableDTO>(m))); // AnalogicVariableDTO de gRPC

            return Task.FromResult(analogicVariableDTOs);
        }

        public override Task<Empty> UpdateAnalogicVariable(AnalogicVariableDTO request, ServerCallContext context)
        {
            var command = new UpdateAnalogicVariableCommand(
                Guid.Parse(request.Id),
                request.Name,
                (VariableType)request.Type, // VariableType del dominio
                request.IsMeasurement,
                request.Code,
                TimeSpan.Parse(request.SamplingPeriod),
                request.ModbusAddress,
                request.Value,
                Guid.Parse(request.Unitid)
            );

            _mediator.Send(command); // Llamada sincrónica
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteAnalogicVariable(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteAnalogicVariableCommand(Guid.Parse(request.Id));
            _mediator.Send(command); // Llamada sincrónica
            return Task.FromResult(new Empty());
        }
    }
}
