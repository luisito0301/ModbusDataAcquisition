using ModbusData.Application.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ModbusData.Sample.Commands.DeleteSample
{
    public record DeleteSampleCommand(Guid VariableId) : ICommand<bool>;
}
