using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Records;
using ModbusData.DataAccess.Contexts;

namespace ModbusData.DataAccess.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly ApplicationContext _context;

        public SampleRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Implementación del método para obtener todas las muestras asociadas a una misma variable
        public IEnumerable<Sample> GetSamplesByVariableId(Guid variableId)
        {
            return _context.Samples
                .Where(s => s.VariableId == variableId)
                .ToList();
        }

        // Implementación del método para agregar una nueva muestra
        public void AddSample(Sample sample)
        {
            _context.Samples.Add(sample);
            _context.SaveChanges();
        }

        // Implementación del método para obtener muestras en un rango de fechas
        public IEnumerable<Sample> GetSamplesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Samples
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();
        }
    }
}
