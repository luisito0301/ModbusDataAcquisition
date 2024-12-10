using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Variables;
using ModbusData.DataAccess.Tests.Utilities; // Asumiendo que tienes una clase ConnectionStringProvider para obtener la cadena de conexión
using System;
using System.Linq;
using ModbusData.Contract.Variables;
using ModbusData.Contract;
using ModbusData.Domain.Types;

namespace ModbusData.DataAccess.Tests
{
    [TestClass]
    public class VariablesTests
    {
        private IVariableRepository _variableRepository;
        private IUnitOfWork _unitOfWork;

        public VariablesTests()
        {
            ApplicationContext context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());
            _variableRepository = new VariableRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [DataRow( "Temperature", VariableType.Analogic, true, "Temp", 5, 100)]
        [DataRow( "Pressure", VariableType.Analogic, false, "Pressure", 10, 101)]
        [TestMethod]
        public void Can_Add_Variable(
           
            string name,
            VariableType type,
            bool isMeasurement,
            string code,
            double samplingPeriodSeconds,
            int modbusAddress)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AnalogicVariable variable = new AnalogicVariable(id, name, type, isMeasurement, code, TimeSpan.FromSeconds(samplingPeriodSeconds), modbusAddress);

            // Execute
            _variableRepository.AddVariable(variable);
            _unitOfWork.SaveChanges();

            // Assert
            AnalogicVariable? loadedVariable = _variableRepository.GetVariableById<AnalogicVariable>(id);
            Assert.IsNotNull(loadedVariable);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Get_Variable_By_Id(int position)
        {
            // Arrange
            var variables = _variableRepository.GetAllVariables<AnalogicVariable>().ToList();
            Assert.IsNotNull(variables);
            Assert.IsTrue(position < variables.Count);
            AnalogicVariable variableToGet = variables[position];

            // Execute
            AnalogicVariable? loadedVariable = _variableRepository.GetVariableById<AnalogicVariable>(variableToGet.Id);

            // Assert
            Assert.IsNotNull(loadedVariable);
        }

        [TestMethod]
        public void Cannot_Get_Variable_By_Invalid_Id()
        {
            // Arrange

            // Execute
            AnalogicVariable? loadedVariable = _variableRepository.GetVariableById<AnalogicVariable>(Guid.Empty);

            // Assert
            Assert.IsNull(loadedVariable);
        }

        [DataRow(0, "New Temperature")]
        [TestMethod]
        public void Can_Update_Variable(int position, string newName)
        {
            // Arrange
            var variables = _variableRepository.GetAllVariables<AnalogicVariable>().ToList();
            Assert.IsNotNull(variables);
            Assert.IsTrue(position < variables.Count);
            AnalogicVariable variableToUpdate = variables[position];

            // Execute
            variableToUpdate.Name = newName;
            _variableRepository.UpdateVariable(variableToUpdate);
            _unitOfWork.SaveChanges();

            // Assert
            AnalogicVariable? loadedVariable = _variableRepository.GetVariableById<AnalogicVariable>(variableToUpdate.Id);
            Assert.IsNotNull(loadedVariable);
            Assert.AreEqual(loadedVariable.Name, newName);
        }

        [DataRow(0)]
        [TestMethod]
        public void Can_Delete_Variable(int position)
        {
            // Arrange
            var variables = _variableRepository.GetAllVariables<AnalogicVariable>().ToList();
            Assert.IsNotNull(variables);
            Assert.IsTrue(position < variables.Count);
            AnalogicVariable variableToDelete = variables[position];

            // Execute
            _variableRepository.DeleteVariable(variableToDelete);
            _unitOfWork.SaveChanges();

            // Assert
            AnalogicVariable? loadedVariable = _variableRepository.GetVariableById<AnalogicVariable>(variableToDelete.Id);
            Assert.IsNull(loadedVariable);
        }
    }
}
