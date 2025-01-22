using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using ModbusData.Application.Unit.Commands.CreateUnit;
using ModbusData.Application.Units.Commands.CreateUnit;
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
    }
}