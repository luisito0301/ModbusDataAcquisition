using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ModbusData.Application.Variables.Commands.CreateAnalogicVariable;
using ModbusData.Application.Variables.Commands.DeleteAnalogicVariable;
using ModbusData.Application.Variables.Commands.UpdateAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAllAnalogicVariable;
using ModbusData.Application.Variables.Queries.GetAnalogicVariable;
using ModbusData.Domain.Entities.Variables;
using ModbusData.GrpcProtos;
using ModbusData.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace AnalogicVariableServiceTests
{
    [TestClass]
    public class AnalogicVariableServiceTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<AnalogicVariableService>> _loggerMock;
        private AnalogicVariableService _service;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<AnalogicVariableService>>();
            _service = new AnalogicVariableService(_mediatorMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void CreateAnalogicVariable_ShouldReturnDTO()
        {
            // Arrange
            var request = new CreateAnalogicVariableRequest
            {
                Name = "Test Variable",
                Type = VariableType.Analogic, // Adjust as necessary
                IsMeasurement = true,
                Code = "TV001",
                SamplingPeriod = "00:00:01", // Example TimeSpan
                ModbusAddress = 123,
                Value = 10.0,
                Unitid = Guid.NewGuid().ToString()
            };

            var commandResult = new ModbusData.Domain.Entities.Variables.AnalogicVariable
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                // Set other properties as needed
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAnalogicVariableCommand>(), default))
                .Returns(commandResult);
            _mapperMock.Setup(m => m.Map<AnalogicVariableDTO>(It.IsAny<ModbusData.Domain.Entities.Variables.AnalogicVariable>()))
                .Returns(new AnalogicVariableDTO { Id = commandResult.Id.ToString(), Name = commandResult.Name });

            // Act
            var result = _service.CreateAnalogicVariable(request, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Name, result.Name);
        }

        [TestMethod]
        public void GetAnalogicVariable_ShouldReturnDTO()
        {
            // Arrange
            var variableId = Guid.NewGuid();
            var query = new GetAnalogicVariableByIdQuery(variableId);
            var variable = new AnalogicVariable { Id = variableId, Name = "Test Variable" };

            _mediatorMock.Setup(m => m.Send(query, default)).Returns(variable);
            _mapperMock.Setup(m => m.Map<AnalogicVariableDTO>(variable))
                .Returns(new AnalogicVariableDTO { Id = variable.Id.ToString(), Name = variable.Name });

            var request = new GetRequest { Id = variableId.ToString() };

            // Act
            var result = _service.GetAnalogicVariable(request, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(variable.Name, result.AnalogicVariable.Name);
        }

        [TestMethod]
        public void UpdateAnalogicVariable_ShouldSucceed()
        {
            // Arrange
            var request = new AnalogicVariableDTO
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Updated Variable",
                Type = VariableType.Analogic, // Adjust as necessary
                IsMeasurement = true,
                Code = "TV002",
                SamplingPeriod = "00:00:02", // Example TimeSpan
                ModbusAddress = 456,
                Value = 20.0,
                Unitid = Guid.NewGuid().ToString()
            };

            var command = new UpdateAnalogicVariableCommand(
                Guid.Parse(request.Id),
                request.Name,
                (ModbusData.Domain.Types.VariableType)request.Type,
                request.IsMeasurement,
                request.Code,
                TimeSpan.Parse(request.SamplingPeriod),
                request.ModbusAddress,
                request.Value,
                Guid.Parse(request.Unitid)
            );

            // Act
            _service.UpdateAnalogicVariable(request, null);

            // Assert
            _mediatorMock.Verify(m => m.Send(command, default), Times.Once);
        }
    }
}

       