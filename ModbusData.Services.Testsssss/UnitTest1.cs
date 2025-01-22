using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusData.Contract;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Tests.Utilities;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using ModbusData.GrpcProtos;
using System;
using System.Collections.Generic;
using System.Linq;
using AnalogicVariable = ModbusData.GrpcProtos.AnalogicVariable;

namespace ModbusData.Services.Testssssss
{
    [TestClass]
    public class AnalogicVariableTests
    {
        private GrpcChannel _channel;
        private AnalogicVariable.AnalogicVariableClient _client;
        private ApplicationContext _context;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Setup()
        {
            // Setup in-memory database
            _context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            // Setup gRPC channel and client
            _channel = GrpcChannel.ForAddress("http://localhost:5051");
            _client = new AnalogicVariable.AnalogicVariableClient(_channel);
        }

        [TestMethod]
        public void CreateAndRetrieveAnalogicVariable()
        {
            // Arrange
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable>()); // Use List<Variable>
            _context.Set<Unit>().Add(unit);
            _unitOfWork.SaveChanges();
            var createRequest = new CreateAnalogicVariableRequest()

            {
                Name = "Test Variable",
                Type = VariableType.Analogic,
                IsMeasurement = true,
                Code = "TV001",
                SamplingPeriod = "00:00:01",
                ModbusAddress = 123,
                Value = 10.0,
                Unitid = unitId.ToString(),
            };

            // Act: Create the variable
            var createResponse = _client.CreateAnalogicVariable(createRequest);
            Assert.IsNotNull(createResponse);
            Assert.IsFalse(string.IsNullOrEmpty(createResponse.Id));

            // Act: Retrieve all variables
            var getResponse = _client.GetAllAnalogicVariables(new Google.Protobuf.WellKnownTypes.Empty());
            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Items.Count > 0);

            // Act: Retrieve by ID
            var getByIdResponse = _client.GetAnalogicVariable(new GetRequest() { Id = createResponse.Id });
            Assert.IsNotNull(getByIdResponse);
            Assert.AreEqual(createRequest.Name, getByIdResponse.AnalogicVariable.Name);
        }

        
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

            if (_channel != null)
                _channel.Dispose();
        }
    }
}
