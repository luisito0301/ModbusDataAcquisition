using NUnit.Framework;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
namespace ModbusData.DataAccess.Tests.Utilities
{
    [TestClass]
    [TestFixture]
    public class UnitTests
    {
        private Unit _unit;
        private Variable _variable;

        [SetUp]
        public void SetUp()
        {
            // Inicializa una nueva instancia de Unit antes de cada prueba
            _variable = new Variable { Id = Guid.NewGuid() }; // Asume que Variable tiene una propiedad Id
            _unit = new Unit(Guid.NewGuid(), "Fabricante1", "Código1", "Área1", new List<Variable>());
        }

        [Test]
        public void AddVariable_ShouldAddVariable_WhenVariableIsNew()
        {
            // Act
            _unit.AddVariable(_variable);

            // Assert
            Assert.Contains(_variable, (System.Collections.ICollection)_unit.Variables);
        }

        [Test]
        public void AddVariable_ShouldThrowException_WhenVariableAlreadyExists()
        {
            // Arrange
            _unit.AddVariable(_variable);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _unit.AddVariable(_variable));
            Assert.That(ex.Message, Is.EqualTo("La variable ya está asociada a esta unidad."));
        }

        [Test]
        public void RemoveVariable_ShouldRemoveVariable_WhenVariableExists()
        {
            // Arrange
            _unit.AddVariable(_variable);

            // Act
            _unit.RemoveVariable(_variable);

            // Assert
            Assert.IsFalse(_unit.Variables.Contains(_variable));
        }

        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var variables = new List<Variable> { _variable };

            // Act
            var unit = new Unit(Guid.NewGuid(), "Fabricante2", "Código2", "Área2", variables);

            // Assert
            Assert.AreEqual("Fabricante2", unit.ManufactererName);
            Assert.AreEqual("Código2", unit.Code);
            Assert.AreEqual("Área2", unit.AreaName);
            Assert.Contains(_variable, (System.Collections.ICollection)unit.Variables);
        }
    }
}
