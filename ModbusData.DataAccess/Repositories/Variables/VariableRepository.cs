using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Variables;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Repositories.Variables
{
    public class VariableRepository<T> : RepositoryBase<T>, Contract.Variables.IVariableRepository<T> where T : Variable
    {
        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public VariableRepository(ApplicationContext context) : base(context)
        {
        }

        /// <summary>Añade una muestra a una variable específica.</summary>
        /// <param name="variableId">El ID de la variable.</param>
        /// <param name="sample">La muestra a añadir.</param>
        public void AddSample(Guid variableId, Sample sample)
        {
            var variable = _context.Variables.Find(variableId);
            if (variable != null)
            {
                variable.Samples.Add(sample);
                _context.SaveChanges();
            }
        }

        /// <summary>Busca muestras por fecha.</summary>
        /// <param name="variableId">El ID de la variable.</param>
        /// <param name="startDate">La fecha de inicio del rango.</param>
        /// <param name="endDate">La fecha de fin del rango.</param>
        /// <returns>Una lista de muestras dentro del rango de fechas.</returns>
        public IEnumerable<Sample> GetSamplesByDate(Guid variableId, DateTime startDate, DateTime endDate)
        {
            var variable = _context.Variables
                .Include(v => v.Samples)
                .FirstOrDefault(v => v.Id == variableId);

            if (variable != null)
            {
                return variable.Samples.Where(s => s.Date >= startDate && s.Date <= endDate);
            }

            return Enumerable.Empty<Sample>();
        }

        /// <summary>Busca muestras en un rango de fechas entre todas las variables.</summary>
        /// <param name="startDate">La fecha de inicio del rango.</param>
        /// <param name="endDate">La fecha de fin del rango.</param>
        /// <returns>Una lista de muestras dentro del rango de fechas.</returns>
        public IEnumerable<Sample> GetSamplesByDateRange(DateTime startDate, DateTime endDate)
        {
            var samples = _context.Variables
                .SelectMany(v => v.Samples)
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();

            return samples;
        }
    }
}
