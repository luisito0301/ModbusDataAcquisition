using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Records
{
    public record Sample
    {
        public Guid VariableId  { get; set; }
        public DateTime Date { get; set; }
    }
}
