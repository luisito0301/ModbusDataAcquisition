using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModbusData.DataAccess.Contexts;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly ApplicationContext _context;

        public SampleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Sample> GetAllSamples()
        {
            return _context.Samples.ToList();
        }

        public Sample GetSample(DateTime date, Guid variableId)
        {
            return _context.Samples
                .FirstOrDefault(s => s.Date == date && s.VariableId == variableId);
        }

        public void AddSample(Sample sample)
        {
            _context.Samples.Add(sample);
            _context.SaveChanges();
        }

        public void UpdateSample(Sample sample)
        {
            var existingSample = _context.Samples
                .FirstOrDefault(s => s.Date == sample.Date && s.VariableId == sample.VariableId);

            if (existingSample != null)
            {
                _context.Entry(existingSample).State = EntityState.Detached; // Desanexar la entidad original
            }

            _context.Samples.Update(sample);
            _context.SaveChanges();
        }


        public void DeleteSample(DateTime fecha, Guid variableId)
        {
            var sample = GetSample(fecha, variableId);
            if (sample != null)
            {
                _context.Samples.Remove(sample);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Sample> GetSamplesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Samples
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();
        }
    }
}
