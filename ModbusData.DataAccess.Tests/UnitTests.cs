using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Entities.Device; // Adjust the namespace as necessary
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Devices; // Adjust the namespace as necessary
using ModbusData.DataAccess.Tests.Utilities; // Assuming you have a class ConnectionStringProvider for getting the connection string
using System;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract;
using ModbusData.Contract.Devices;
using ModbusData.Domain.Entities.Unit;
using ModbusData.DataAccess;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Repositories.Units;
using ModbusData.Contract.Units;
using ModbusData.Domain.Types; // Importar el enum UnitType

namespace ModbusData.DataAccess.Tests
{
    [TestClass]
    public class UnitTests
    {
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;
        private IUnitRepository<Unit> _unitRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _unitOfWork = new UnitOfWork(_context); // Inicializa la unidad de trabajo
            _unitRepository = new UnitRepository<Unit>(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public void Add_ShouldAddUnit()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Continuous);

            // Act
            _unitRepository.Add(unit);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _context.Set<Unit>().FirstOrDefault(u => u.Id == unitId);
            Assert.IsNotNull(result);
            Assert.AreEqual("Main Unit Manufacturer", result.ManufactererName);
            Assert.AreEqual(UnitType.Continuous, result.UnitTypes);
        }

        [TestMethod]
        public void GetById_ShouldReturnUnit()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Batch);
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges();

            // Act
            var result = _unitRepository.GetById(unitId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Main Unit Manufacturer", result.ManufactererName);
            Assert.AreEqual(UnitType.Batch, result.UnitTypes);
        }

        [TestMethod]
        public void Cannot_Get_Unit_By_Invalid_Id()
        {
            // Arrange
            var invalidId = Guid.NewGuid(); // Generate a new GUID that does not exist in the database

            // Act
            var result = _unitRepository.GetById(invalidId);

            // Assert
            Assert.IsNull(result); // The result should be null since the ID is invalid
        }

        [TestMethod]
        public void Update_ShouldModifyUnit()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Storage);
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges();

            // Act
            var updatedUnit = new Unit(unitId, "Updated Unit Manufacturer", "MU002", "Updated Factory", UnitType.Discrete);
            _unitRepository.Update(updatedUnit);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _context.Set<Unit>().Find(unitId);
            Assert.AreEqual("Updated Unit Manufacturer", result.ManufactererName);
            Assert.AreEqual(UnitType.Discrete, result.UnitTypes);
        }

        [TestMethod]
        public void Delete_ShouldRemoveUnit()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", UnitType.Continuous);
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges();

            // Act
            _unitRepository.Delete(unitId);
            _unitOfWork.SaveChanges();

            // Assert
            var result = _context.Set<Unit>().Find(unitId);
            Assert.IsNull(result);
        }
    }
}
