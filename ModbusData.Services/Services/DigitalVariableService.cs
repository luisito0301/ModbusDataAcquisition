using AutoMapper;
using ModbusData.Application.Variables.Commands.CreateDigitalVariable;
using ModbusData.Application.Variables.Commands.UpdateDigitalVariable;
using ModbusData.Application.Variables.Queries.GetAllDigitalVariable;
using ModbusData.Application.Variables.Queries.GetDigitalVariable;
using ModbusData.Domain.Types;
using ModbusData.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Variables.Commands.DeleteDigitalVariable;


namespace ModbusData.Services.Services
{
    public class DigitalVariableService : GrpcProtos.DigitalVariable.DigitalVariableBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DigitalVariableService> _logger;

        public DigitalVariableService(IMediator mediator, IMapper mapper, ILogger<DigitalVariableService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<DigitalVariableDTO> CreateDigitalVariable(CreateDigitalVariableRequest request, ServerCallContext context)
        {
            var command = new CreateDigitalVariableCommand(
                request.Name,
                (Domain.Types.VariableType)request.Type,
                request.IsMeasurement,
                request.Code,
                TimeSpan.Parse(request.SamplingPeriod),
                request.ModbusAddress,
                Guid.Parse(request.Unitid)
            );

            var result = _mediator.Send(command).Result; // Synchronous call
            return Task.FromResult(_mapper.Map<DigitalVariableDTO>(result));
        }

        public override Task<NullableDigitalVariableDTO> GetDigitalVariable(GetRequest request, ServerCallContext context)
        {
            var query = new GetDigitalVariableByIdQuery(Guid.Parse(request.Id));
            var result = _mediator.Send(query).Result; // Synchronous call

            if (result is null)
            {
                _logger.LogWarning("DigitalVariable not found for ID: {DigitalVariableId}", request.Id);
                return Task.FromResult(new NullableDigitalVariableDTO() { Null = NullValue.NullValue });
            }

            _logger.LogInformation("DigitalVariable found for ID: {DigitalVariableId}", request.Id);
            return Task.FromResult(new NullableDigitalVariableDTO { DigitalVariable = _mapper.Map<DigitalVariableDTO>(result) });
        }

        public override Task<DigitalVariables> GetAllDigitalVariables(Empty request, ServerCallContext context)
        {
            var query = new GetAllDigitalVariableQuery();
            var result = _mediator.Send(query).Result; // Synchronous call

            var digitalVariableDTOs = new DigitalVariables();
            digitalVariableDTOs.Items.AddRange(result.Select(m => _mapper.Map<DigitalVariableDTO>(m)));

            return Task.FromResult(digitalVariableDTOs); ;
        }

        public override Task<Empty> UpdateDigitalVariable(DigitalVariableDTO request, ServerCallContext context)
        {
            var command = new UpdateDigitalVariableCommand(
                Guid.Parse(request.Id),
                request.Name,
                (Domain.Types.VariableType)request.Type,
                request.IsMeasurement,
                request.Code,
                TimeSpan.Parse(request.SamplingPeriod),
                request.ModbusAddress,
                Guid.Parse(request.Unitid)
            );

            _mediator.Send(command); // Synchronous call
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteDigitalVariable(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteDigitalVariableCommand(Guid.Parse(request.Id));
            _mediator.Send(command); // Synchronous call
            return Task.FromResult(new Empty());
        }
    }
}
