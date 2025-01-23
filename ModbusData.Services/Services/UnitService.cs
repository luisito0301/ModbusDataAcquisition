using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using ModbusData.Application.Unit.Commands.CreateUnit;
using ModbusData.Unit.Commands.DeleteUnit;
using ModbusData.Application.Unit.Commands.UpdateUnit;
using ModbusData.Application.Unit.Queries.GetAllUnit;
using ModbusData.Application.Unit.Queries.GetUnit;
using ModbusData.GrpcProtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModbusData.Services.Services
{
    public class UnitService : GrpcProtos.Unit.UnitBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UnitService> _logger;

        public UnitService(IMediator mediator, IMapper mapper, ILogger<UnitService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override  Task<UnitDTO> CreateUnit(CreateUnitRequest request, ServerCallContext context)
        {
            // Mapear el request a un comando
            var command = new CreateUnitCommand(
                request.ManufactererName,
                request.Code,
                request.AreaName,
                
                request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList() // Asegúrate de que AnalogicVariable esté mapeado
                , (Domain.Types.UnitType)request.Types_
            );

            var result = _mediator.Send(command).Result; // Synchronous call
            return Task.FromResult(_mapper.Map<UnitDTO>(result));
        }
        public override Task<NullableUnitDTO> GetUnit(GetRequest request, ServerCallContext context)
        {
            var query = new GetUnitByIdQuery(Guid.Parse(request.Id));
            var result = _mediator.Send(query).Result; // Synchronous call

            if (result is null)
            {
                _logger.LogWarning("Unit not found for ID: {UnitId}", request.Id);
                return Task.FromResult(new NullableUnitDTO() { Null = NullValue.NullValue });
            }

            _logger.LogInformation("Unit found for ID: {UnitId}", request.Id);
            return Task.FromResult(new NullableUnitDTO { Unit = _mapper.Map<UnitDTO>(result) });
        }

        public override Task<ModbusData.GrpcProtos.Units> GetAllUnits(Empty request, ServerCallContext context)
        {
            var query = new GetAllUnitQuery();
            var result = _mediator.Send(query).Result; // Synchronous call

            var unitDTOs = new Units();
            unitDTOs.Items.AddRange(result.Select(m => _mapper.Map<UnitDTO>(m)));

            return Task.FromResult(unitDTOs); ;
        }

        public override Task<Empty> UpdateUnit(UnitDTO request, ServerCallContext context)
        {
            var command = new UpdateUnitCommand(
                Guid.Parse(request.Id),
                request.ManufactererName,
                request.Code,
                request.AreaName,
                request.Variables.Select(v => _mapper.Map<ModbusData.Domain.Entities.Variables.Variable>(v)).ToList(),
               (Domain.Types.UnitType)request.Types_
            );

            _mediator.Send(command); // Synchronous call
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteUnit(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteUnitCommand(Guid.Parse(request.Id));
            _mediator.Send(command); // Synchronous call
            return Task.FromResult(new Empty());
        }
    }
}