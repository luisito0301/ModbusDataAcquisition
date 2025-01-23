using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Application.Sample.GetSamplebyId
{
    public record GetSampleByVariableIdQuery(Guid Id) : IRequest<ModbusData.Domain.Records.Sample>;
}
