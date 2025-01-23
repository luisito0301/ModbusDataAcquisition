using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using MediatR;
using ModbusData.Domain.Records;

namespace ModbusData.Application.Sample.Queries.GetAllSamples
{
    public record GetAllSamplesQuery : IRequest<List<GetAllSamplesQuery>>;
}