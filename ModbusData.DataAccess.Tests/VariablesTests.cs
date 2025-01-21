using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Variables;
using ModbusData.DataAccess.Tests.Utilities; // Assuming you have a class ConnectionStringProvider for getting the connection string
using System;
using System.Linq;
using ModbusData.Contract.Variables;
using ModbusData.Contract;
using ModbusData.Domain.Types;
using ModbusData.DataAccess;
using ModbusData.Domain.Entities.Unit;
using System.Collections.Generic;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Tests
{
    /// <summary>Clase de pruebas unitarias para VariableRepository.</summary>
    [TestClass]
    public class VariablesTests
    {
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;
        private IVariableRepository<AnalogicVariable> _variableRepository;

        /// <summary>Constructor que inicializa el contexto y el repositorio.</summary>
        [TestInitialize]
        public void SetUp()
        {
            // Use a real database connection for testing
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _unitOfWork = new UnitOfWork(_context); // Inicializa la unidad de trabajo
            _variableRepository = new VariableRepository<AnalogicVariable>(_context);

            // Clean up and recreate the database
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        /// <summary>Prueba para verificar que Add agrega una variable.</summary>
        [TestMethod]
        public void Add_ShouldAddVariable()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Continuous); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable = new AnalogicVariable(Guid.NewGuid(), "Temperature", VariableType.Analogic, true, "Temp", TimeSpan.FromSeconds(5), 100)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };

            // Act
            _variableRepository.Add(variable);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _context.Set<AnalogicVariable>().FirstOrDefault(v => v.Name == "Temperature");
            Assert.IsNotNull(result);
            Assert.AreEqual("Temperature", result.Name);
        }

        /// <summary>Prueba para verificar que GetById devuelve una variable.</summary>
        [TestMethod]
        public void GetById_ShouldReturnVariable()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Batch); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable = new AnalogicVariable(Guid.NewGuid(), "Pressure", VariableType.Analogic, true, "Pressure", TimeSpan.FromSeconds(10), 101)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };

            _context.Set<AnalogicVariable>().Add(variable);
            _unitOfWork.SaveChanges();

            // Act
            var result = _variableRepository.GetById(variable.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pressure", result.Name);
        }

        /// <summary>Prueba para verificar que GetAll devuelve todas las variables.</summary>
        [TestMethod]
        public void GetAll_ShouldReturnAllVariables()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Discrete); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable1 = new AnalogicVariable(Guid.NewGuid(), "Temperature", VariableType.Analogic, true, "Temp", TimeSpan.FromSeconds(5), 100)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };

            var variable2 = new AnalogicVariable(Guid.NewGuid(), "Pressure", VariableType.Analogic, true, "Pressure", TimeSpan.FromSeconds(10), 101)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };

            _context.Set<AnalogicVariable>().AddRange(variable1, variable2);
            _unitOfWork.SaveChanges(); // Save the variables

            // Act
            var result = _variableRepository.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>Prueba para verificar que Update modifica una variable.</summary>
        [TestMethod]
        public void Update_ShouldModifyVariable()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Storage); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable = new AnalogicVariable(Guid.NewGuid(), "Temperature", VariableType.Analogic, true, "Temp", TimeSpan.FromSeconds(5), 100)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };

            _context.Set<AnalogicVariable>().Add(variable);
            _unitOfWork.SaveChanges(); // Save the variable first

            // Act
            var updatedVariable = new AnalogicVariable(variable.Id, "Updated Temperature", variable.Type, variable.IsMeasurement, variable.Code, variable.SamplingPeriod, variable.ModbusAddress)
            {
                UnitId = unitId // Ensure the foreign key is set
            };

            _variableRepository.Update(updatedVariable);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _context.Set<AnalogicVariable>().Find(variable.Id);
            Assert.AreEqual("Updated Temperature", result.Name);
        }

        [TestMethod]
        public void Cannot_Get_Variable_By_Invalid_Id()
        {
            // Arrange
            var invalidId = Guid.NewGuid(); // Generate a new GUID that does not exist in the database

            // Act
            var result = _variableRepository.GetById(invalidId);

            // Assert
            Assert.IsNull(result); // The result should be null since the ID is invalid
        }

        /// <summary>Prueba para verificar que AddSample agrega una muestra a una variable.</summary>
        [TestMethod]
        public void AddSample_ShouldAddSampleToVariable()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Continuous); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable = new AnalogicVariable(Guid.NewGuid(), "Humidity", VariableType.Analogic, true, "Humidity", TimeSpan.FromSeconds(15), 102)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };
            _context.Set<AnalogicVariable>().Add(variable);
            _unitOfWork.SaveChanges(); // Save the variable first to ensure it exists

            var sample = new Sample
            {
                Date = DateTime.Now
            };

            // Act
            _variableRepository.AddSample(variable.Id, sample);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _variableRepository.GetById(variable.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Samples.Count);
            Assert.AreEqual(sample.Date, result.Samples.First().Date);
        }

        /// <summary>Prueba para verificar que GetSamplesByDate devuelve las muestras en un rango de fechas.</summary>
        [TestMethod]
        public void GetSamplesByDate_ShouldReturnSamplesInDateRange()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Batch); // Incluye UnitType
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges(); // Save the unit first to ensure it exists

            var variable = new AnalogicVariable(Guid.NewGuid(), "Humidity", VariableType.Analogic, true, "Humidity", TimeSpan.FromSeconds(15), 102)
            {
                UnitId = unitId // Set the foreign key to the existing unit
            };
            _context.Set<AnalogicVariable>().Add(variable);
            _unitOfWork.SaveChanges(); // Save the variable first to ensure it exists

            var sample1 = new Sample
            {
                Date = DateTime.Now.AddDays(-1)
            };
            var sample2 = new Sample
            {
                Date = DateTime.Now
            };

            _variableRepository.AddSample(variable.Id, sample1);
            _variableRepository.AddSample(variable.Id, sample2);
            _unitOfWork.SaveChanges();

            // Act
            var result = _variableRepository.GetSamplesByDate(variable.Id, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1)).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(s => s.Date == sample1.Date));
            Assert.IsTrue(result.Any(s => s.Date == sample2.Date));
        }
    }
}
//