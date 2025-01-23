using ModbusData.Application.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusData.Domain.Records;

namespace ModbusData.Application.Sample.Commands.CreateSample
{
    public record CreateSampleCommand(
        string VariableId,  // Identificador de la variable asociada a la muestra
        DateTime Date,      // Fecha en que se tomó la muestra
        double Value        // Valor de la muestra
    ) : ICommand<ModbusData.Domain.Records.Sample>;
}