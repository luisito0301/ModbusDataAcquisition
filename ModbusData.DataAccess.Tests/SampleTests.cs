using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ModbusData.Domain.Records;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories;

namespace ModbusData.DataAccess.Tests
{
    /// <summary>Clase de pruebas unitarias para SampleRepository.</summary>
    [TestClass]
    public class SampleTests
    {
        private ApplicationContext _context;
        private ISampleRepository _sampleRepository;

        /// <summary>Constructor que inicializa el contexto y el repositorio.</summary>
        [TestInitialize]
        public void SetUp()
        {
            // Use a real database connection for testing
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite("Data Source=sampletest.db")
                .Options;

            _context = new ApplicationContext(options);
            _sampleRepository = new SampleRepository(_context);

            // Clean up and recreate the database
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>Prueba para verificar que Add agrega un sample.</summary>
        [TestMethod]
        public void AddSample_ShouldAddSample()
        {
            // Arrange
            var sample = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 123.45
            };

            // Act
            _sampleRepository.AddSample(sample);
            _context.SaveChanges();

            // Assert
            var result = _context.Samples.FirstOrDefault(s => s.VariableId == sample.VariableId && s.Date == sample.Date);
            Assert.IsNotNull(result);
            Assert.AreEqual(123.45, result.Value);
        }

        /// <summary>Prueba para verificar que GetSamplesByVariableId devuelve las muestras correctas.</summary>
        [TestMethod]
        public void GetSamplesByVariableId_ShouldReturnSamples()
        {
            // Arrange
            var variableId = Guid.NewGuid();
            var sample1 = new Sample
            {
                VariableId = variableId,
                Date = DateTime.Now,
                Value = 123.45
            };
            var sample2 = new Sample
            {
                VariableId = variableId,
                Date = DateTime.Now.AddHours(1),
                Value = 678.90
            };

            _context.Samples.AddRange(sample1, sample2);
            _context.SaveChanges();

            // Act
            var result = _sampleRepository.GetSamplesByVariableId(variableId).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(s => s.Value == 123.45));
            Assert.IsTrue(result.Any(s => s.Value == 678.90));
        }

        /// <summary>Prueba para verificar que GetSamplesByDateRange devuelve las muestras en el rango de fechas.</summary>
        [TestMethod]
        public void GetSamplesByDateRange_ShouldReturnSamples()
        {
            // Arrange
            var sample1 = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(-1),
                Value = 123.45
            };
            var sample2 = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 678.90
            };

            _context.Samples.AddRange(sample1, sample2);
            _context.SaveChanges();

            // Act
            var result = _sampleRepository.GetSamplesByDateRange(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1)).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(s => s.Value == 123.45));
            Assert.IsTrue(result.Any(s => s.Value == 678.90));
        }
    }
}
