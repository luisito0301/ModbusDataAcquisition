using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ModbusData.GrpcProtos;

using System;
using System.Linq;
using System.Threading.Tasks;
using ModbusData.Application.Sample.GetSample;
using ModbusData.Application.Sample.CreateSample;

namespace ModbusData.Services.Services
{
    public class SampleService : ModbusData.GrpcProtos.SampleService.SampleServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SampleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override Task<Empty> CreateSample(CreateSampleRequest request, ServerCallContext context)
        {
            var command = new CreateSampleCommand(
                request.VariableId,
                request.Date,
                request.Value
            );

            _mediator.Send(command).Wait();
            return Task.FromResult(new Empty());
        }

        public override Task<Samples> GetSamplesByVariableId(GetSamplesByVariableIdRequest request, ServerCallContext context)
        {
            var query = new GetSamplesByVariableIdQuery(Guid.Parse(request.VariableId));
            var result = _mediator.Send(query).Result;

            var samplesDTO = new Samples();
            samplesDTO.Items.AddRange(result.Select(m => _mapper.Map<SampleDTO>(m)));

            return Task.FromResult(samplesDTO);
        }

        public override Task<Samples> GetSamplesByDateRange(GetSamplesByDateRangeRequest request, ServerCallContext context)
        {
            var query = new GetSamplesByDateRangeQuery(
                DateTime.Parse(request.StartDate),
                DateTime.Parse(request.EndDate)
            );
            var result = _mediator.Send(query).Result;

            var samplesDTO = new Samples();
            samplesDTO.Items.AddRange(result.Select(m => _mapper.Map<SampleDTO>(m)));

            return Task.FromResult(samplesDTO);
        }
    }
}
