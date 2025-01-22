using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories;
using ModbusData.Domain.Records;

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
        public void Add_ShouldAddSample()
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

        /// <summary>Prueba para verificar que GetSample devuelve un sample.</summary>
        [TestMethod]
        public void GetSample_ShouldReturnSample()
        {
            // Arrange
            var sample = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 123.45
            };
            _context.Samples.Add(sample);
            _context.SaveChanges();

            // Act
            var result = _sampleRepository.GetSample(sample.Date, sample.VariableId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(123.45, result.Value);
        }

        /// <summary>Prueba para verificar que GetAll devuelve todos los samples.</summary>
        [TestMethod]
        public void GetAll_ShouldReturnAllSamples()
        {
            // Arrange
            var sample1 = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 123.45
            };
            var sample2 = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now.AddMinutes(-5),
                Value = 678.90
            };

            _context.Samples.AddRange(sample1, sample2);
            _context.SaveChanges();

            // Act
            var result = _sampleRepository.GetAllSamples().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>Prueba para verificar que Update modifica un sample.</summary>
        [TestMethod]
        public void Update_ShouldModifySample()
        {
            // Arrange
            var sample = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 123.45
            };
            _context.Samples.Add(sample);
            _context.SaveChanges();

            // Act
            var updatedSample = new Sample
            {
                VariableId = sample.VariableId,
                Date = sample.Date,
                Value = 543.21
            };
            _sampleRepository.UpdateSample(updatedSample);
            _context.SaveChanges();

            // Assert
            var result = _context.Samples.FirstOrDefault(s => s.VariableId == sample.VariableId && s.Date == sample.Date);
            Assert.AreEqual(543.21, result.Value);
        }

        /// <summary>Prueba para verificar que Delete elimina un sample.</summary>
        [TestMethod]
        public void Delete_ShouldRemoveSample()
        {
            // Arrange
            var sample = new Sample
            {
                VariableId = Guid.NewGuid(),
                Date = DateTime.Now,
                Value = 123.45
            };
            _context.Samples.Add(sample);
            _context.SaveChanges();

            // Act
            _sampleRepository.DeleteSample(sample.Date, sample.VariableId);
            _context.SaveChanges();

            // Assert
            var result = _context.Samples.FirstOrDefault(s => s.VariableId == sample.VariableId && s.Date == sample.Date);
            Assert.IsNull(result);
        }

        /// <summary>Prueba para verificar que GetSamplesByDateRange devuelve los samples en el rango de fechas.</summary>
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
        }
    }
}
