using ModbusData.Contract;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.DataAccess.Repositories.ModbusNetworks;
using ModbusData.Domain.Entities.Device;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusData.Domain.Entities.Modbus_Network; // Adjust the namespace as necessary
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Tests.Utilities; // Assuming you have a class ConnectionStringProvider for getting the connection string
using ModbusData.Domain.ValueObjects; // Ensure you have the IP class available
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModbusData.DataAccess.Tests
{
   
        [TestClass]
        public class ModbusNetworkTests
        {
            private ApplicationContext _context;
            private IUnitOfWork _unitOfWork;
            private IModbusNetworkRepository<ModbusNetwork> _modbusNetworkRepository; // Use the correct repository interfacee

            [TestInitialize]
            public void SetUp()
            {
                _context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());
                _unitOfWork = new UnitOfWork(_context); // Inicializa la unidad de trabajo
                _modbusNetworkRepository = new ModbusNetworkRepository<ModbusNetwork>(_context); // Use the correct repository

                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            }

            [TestMethod]
            public void Add_ShouldAddModbusNetwork()
            {
                // Arrange
                var networkId = Guid.NewGuid();
                var masterIpAddress = new IP(192, 168, 1, 1); // Assuming you have a constructor for IP
                var modbusNetwork = new ModbusNetwork(networkId, masterIpAddress, new List<SlaveDevice>());

                // Act
                _modbusNetworkRepository.Add(modbusNetwork);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<ModbusNetwork>().FirstOrDefault(n => n.Id == networkId);
                Assert.IsNotNull(result);
                Assert.AreEqual(masterIpAddress.ToString(), result.MasterIpAddress.ToString());
            }

            [TestMethod]
            public void GetById_ShouldReturnModbusNetwork()
            {
                // Arrange
                var networkId = Guid.NewGuid();
                var masterIpAddress = new IP(192, 168, 1, 1);
                var modbusNetwork = new ModbusNetwork(networkId, masterIpAddress, new List<SlaveDevice>());
                _context.Set<ModbusNetwork>().Add(modbusNetwork);
                _unitOfWork.SaveChanges();

                // Act
                var result = _modbusNetworkRepository.GetById(networkId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(masterIpAddress.ToString(), result.MasterIpAddress.ToString());
            }

            [TestMethod]
            public void Cannot_Get_ModbusNetwork_By_Invalid_Id()
            {
                // Arrange
                var invalidId = Guid.NewGuid(); // Generate a new GUID that does not exist in the database

                // Act
                var result = _modbusNetworkRepository.GetById(invalidId);

                // Assert
                Assert.IsNull(result); // The result should be null since the ID is invalid
            }

            [TestMethod]
            public void Update_ShouldModifyModbusNetwork()
            {
                // Arrange
                var networkId = Guid.NewGuid();
                var masterIpAddress = new IP(192, 168, 1, 1);
                var modbusNetwork = new ModbusNetwork(networkId, masterIpAddress, new List<SlaveDevice>());
                _context.Set<ModbusNetwork>().Add(modbusNetwork);
                _unitOfWork.SaveChanges();

                // Act
                var updatedIpAddress = new IP(192, 168, 1, 2);
                var updatedNetwork = new ModbusNetwork(networkId, updatedIpAddress, new List<SlaveDevice>());
                _modbusNetworkRepository.Update(updatedNetwork);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<ModbusNetwork>().Find(networkId);
                Assert.AreEqual(updatedIpAddress.ToString(), result.MasterIpAddress.ToString());
            }

            [TestMethod]
            public void Delete_ShouldRemoveModbusNetwork()
            {
                // Arrange
                var networkId = Guid.NewGuid();
                var masterIpAddress = new IP(192, 168, 1, 1);
                var modbusNetwork = new ModbusNetwork(networkId, masterIpAddress, new List<SlaveDevice>());
                _context.Set<ModbusNetwork>().Add(modbusNetwork);
                _unitOfWork.SaveChanges();

                // Act
                _modbusNetworkRepository.Delete(networkId);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<ModbusNetwork>().Find(networkId);
                Assert.IsNull(result); // The result should be null since the network has been deleted
            }
        } } 