using System;
using System.Collections.Generic;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Repositories
{
    public interface ISampleRepository
    {
        // Método para obtener todas las muestras asociadas a una misma variable
        IEnumerable<Sample> GetSamplesByVariableId(Guid variableId);

        // Método para agregar una nueva muestra
        void AddSample(Sample sample);

        // Método para obtener muestras en un rango de fechas
        IEnumerable<Sample> GetSamplesByDateRange(DateTime startDate, DateTime endDate);
    }
}
