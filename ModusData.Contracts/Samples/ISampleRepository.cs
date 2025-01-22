using System;
using System.Collections.Generic;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Repositories
{
    public interface ISampleRepository
    {
        IEnumerable<Sample> GetAllSamples();
        Sample GetSample(DateTime fecha, Guid variableId);
        void AddSample(Sample sample);
        void UpdateSample(Sample sample);
        void DeleteSample(DateTime fecha, Guid variableId);
        IEnumerable<Sample> GetSamplesByDateRange(DateTime startDate, DateTime endDate); // Nuevo método
    }
}
