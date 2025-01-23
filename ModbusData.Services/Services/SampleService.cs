using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ModbusData.Application.Sample.Commands.CreateSample;
using ModbusData.Application.Sample.Queries.GetAllSamples;
using ModbusData.Application.Sample.Queries.GetSample;
using ModbusData.GrpcProtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModbusData.Services.Services
{
    public class SampleService : GrpcProtos.Sample.SampleBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SampleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override Task<SampleDTO> CreateSample(CreateSampleRequest request, ServerCallContext context)
        {
            // Mapear el request a un comando
            var command = new CreateSampleCommand(
                request.VariableId,
                request.Date.ToDateTime(), // Convertir Timestamp a DateTime
                request.Value
            );

            var result = _mediator.Send(command).Result; // Llamada sincrónica
            return Task.FromResult(_mapper.Map<SampleDTO>(result));
        }

        public override Task<NullableSampleDTO> GetSample(GetSampleRequest request, ServerCallContext context)
        {
            var query = new GetSampleQuery(request.VariableId);
            var result = _mediator.Send(query).Result; // Llamada sincrónica

            if (result is null)
            {
                return Task.FromResult(new NullableSampleDTO() { Null = NullValue.NullValue });
            }

            return Task.FromResult(new NullableSampleDTO { Sample = _mapper.Map<SampleDTO>(result) });
        }

        public override Task<Samples> GetAllSamples(Empty request, ServerCallContext context)
        {
            var query = new GetAllSamplesQuery();
            var result = _mediator.Send(query).Result; // Llamada sincrónica

            var samplesDTOs = new Samples();
            samplesDTOs.Items.AddRange(result.Select(m => _mapper.Map<SampleDTO>(m)));

            return Task.FromResult(samplesDTOs);
        }

        public override Task<Empty> DeleteSample(DeleteSampleRequest request, ServerCallContext context)
        {
            var command = new DeleteSampleCommand(request.VariableId);
            _mediator.Send(command); // Llamada sincrónica
            return Task.FromResult(new Empty());
        }
    }
}