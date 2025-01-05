using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ModbusData.Services;
using ModbusData.Contract;
using ModbusData.Application.Variables.Commands.CreateAnalogicVariable;
using ModbusData.Application.Variables.Commands.UpdateAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAllAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAnalogicVariable;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.GrpcProtos;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using ModbusData.Variables.Commands.DeleteAnalogicVariable;
using ModbusData.Domain.Types;

namespace ModbusData.Services
{
    public class AnalogicVariableService : AnalogicVariable.AnalogicVariableBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AnalogicVariableService> _logger;

        public AnalogicVariableService(
            IMediator mediator,
            IMapper mapper,
            ILogger<AnalogicVariableService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<AnalogicVariableDTO> CreateAnalogicVariable(CreateAnalogicVariableRequest request, ServerCallContext context)
        {
            var command = new CreateAnalogicVariableCommand(
                request.Name,
                (VariableType)request.Type,
                request.IsMeasurement,
                request.Code,
                TimeSpan.FromMilliseconds(request.SamplingPeriod),
                request.ModbusAddress,
                request.Value,
                Guid.Parse(request.UnitId)
            );

            var result = _mediator.Send(command).Result;

            return Task.FromResult(_mapper.Map<AnalogicVariableDTO>(result));
        }

        public override Task<NullableAnalogicVariableDTO> GetAnalogicVariable(GetAnalogicVariableByIdRequest request, ServerCallContext context)
        {
            var query = new GetAnalogicVariableByIdQuery(Guid.Parse(request.Id));

            var result = _mediator.Send(query).Result;

            if (result == null)
            {
                _logger.LogWarning("AnalogicVariable not found for ID: {AnalogicVariableId}", request.Id);
                return Task.FromResult<NullableAnalogicVariableDTO>(null);
            }
            else
            {
                _logger.LogInformation("AnalogicVariable found for ID: {AnalogicVariableId}", request.Id);
            }

            return Task.FromResult(_mapper.Map<NullableAnalogicVariableDTO>(result));
        }

        public override Task<AnalogicVariable> GetAllAnalogicVariables(Empty request, ServerCallContext context)
        {
            var query = new GetAllAnalogicVariableQuery();

            var result = _mediator.Send(query).Result;

            var analogicVariableDTOs = _mapper.Map<List<AnalogicVariableDTO>>(result);

            var analogicVariablesResponse = new AnalogicVariables
            {
                Items = { analogicVariableDTOs } // Assuming Items is a repeated field
            };

            return Task.FromResult(analogicVariablesResponse);
        }

        public override Task<Empty> UpdateAnalogicVariable(AnalogicVariableDTO request, ServerCallContext context)
        {
            var command = new UpdateAnalogicVariableCommand(
                Guid.Parse(request.Id),
                request.Name,
                (VariableType)request.Type,
                request.IsMeasurement,
                request.Code,
                TimeSpan.FromMilliseconds(request.SamplingPeriod),
                request.ModbusAddress,
                request.Value,
                Guid.Parse(request.UnitId)
            );

            var result = _mediator.Send(command).Result;

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteAnalogicVariable(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteAnalogicVariableCommand(Guid.Parse(request.Id));

            var result = _mediator.Send(command).Result;

            return Task.FromResult(new Empty());
        }
    }
}