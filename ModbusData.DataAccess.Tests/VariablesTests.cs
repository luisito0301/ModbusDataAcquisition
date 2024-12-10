using ModbusData.Contract.Variables;
using ModbusData.Contract;
using ModbusData.Domain.Types;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Variables;
using ModbusData.DataAccess.Tests.Utilities; // Asumiendo que tienes una clase ConnectionStringProvider para obtener la cadena de conexión
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ModbusData.DataAccess.Tests
{
        // Clase concreta para pruebas, derivada de Variable
        public class TestVariable : Variable
        {
            public TestVariable(Guid id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
                : base(id, name, type, isMeasurement, code, samplingPeriod, modbusAddress)
            {
            }
        }

        [TestFixture]
        public class VariableTests
        {
            private TestVariable _variable;

            [SetUp]
            public void SetUp()
            {
                // Inicializa una nueva instancia de TestVariable antes de cada prueba
                _variable = new TestVariable(Guid.NewGuid(), "Test Variable", VariableType.Analog, true, "TV001", TimeSpan.FromSeconds(10), 1);
            }

            [Test]
            public void Constructor_ShouldInitializePropertiesCorrectly()
            {
                // Assert
                Assert.AreEqual("Test Variable", _variable.Name);
                Assert.AreEqual(VariableType.Analog, _variable.Type);
                Assert.IsTrue(_variable.IsMeasurement);
                Assert.AreEqual("TV001", _variable.Code);
                Assert.AreEqual(TimeSpan.FromSeconds(10), _variable.SamplingPeriod);
                Assert.AreEqual(1, _variable.ModbusAddress);
            }

            [Test]
            public void Properties_ShouldBeSetAndGetCorrectly()
            {
                // Act
                _variable.Name = "Updated Variable";
                _variable.Type = VariableType.Digital;
                _variable.IsMeasurement = false;
                _variable.Code = "TV002";
                _variable.SamplingPeriod = TimeSpan.FromSeconds(5);
                _variable.ModbusAddress = 2;

                // Assert
                Assert.AreEqual("Updated Variable", _variable.Name);
                Assert.AreEqual(VariableType.Digital, _variable.Type);
                Assert.IsFalse(_variable.IsMeasurement);
                Assert.AreEqual("TV002", _variable.Code);
                Assert.AreEqual(TimeSpan.FromSeconds(5), _variable.SamplingPeriod);
                Assert.AreEqual(2, _variable.ModbusAddress);
            }
        }
    }
