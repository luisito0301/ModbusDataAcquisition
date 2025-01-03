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

namespace ModbusData.Tests.DataAccess.Repositories.Variables
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
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable>()); // Use List<Variable>
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
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable>());
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
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable>());
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
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable>());
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
    }
}

        /// <