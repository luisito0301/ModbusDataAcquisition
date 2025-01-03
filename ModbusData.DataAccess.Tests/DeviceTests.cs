using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusData.Contract;
using ModbusData.Contract.Devices;
using ModbusData.DataAccess;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Devices;
using ModbusData.DataAccess.Tests.Utilities;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.Tests
{
 

        [TestClass]
        public class SlaveDeviceTests
        {
            private ApplicationContext _context;
            private IUnitOfWork _unitOfWork;
            private IDeviceRepository<SlaveDevice> _slaveDeviceRepository;

            [TestInitialize]
            public void SetUp()
            {
                _context = new ApplicationContext(ConnectionStringProvider.GetConnectionString());
                _unitOfWork = new UnitOfWork(_context); // Inicializa la unidad de trabajo
                _slaveDeviceRepository = new DeviceRepository<SlaveDevice>(_context);

                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            }

            [TestMethod]
            public void Add_ShouldAddSlaveDevice()
            {
                // Arrange
                var deviceId = Guid.NewGuid();
                var ipAddress = new IP(192, 168, 1, 100);
                var slaveDevice = new SlaveDevice(deviceId, ipAddress, new List<Variable>());

                // Act
                _slaveDeviceRepository.Add(slaveDevice);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<SlaveDevice>().FirstOrDefault(d => d.Id == deviceId);
                Assert.IsNotNull(result);
                Assert.AreEqual(ipAddress.ToString(), result.IpAddress.ToString());
            }

            [TestMethod]
            public void GetById_ShouldReturnSlaveDevice()
            {
                // Arrange
                var deviceId = Guid.NewGuid();
                var ipAddress = new IP(192, 168, 1, 100);
                var slaveDevice = new SlaveDevice(deviceId, ipAddress, new List<Variable>());
                _context.Set<SlaveDevice>().Add(slaveDevice);
                _unitOfWork.SaveChanges();

                // Act
                var result = _slaveDeviceRepository.GetById(deviceId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(ipAddress.ToString(), result.IpAddress.ToString());
            }

            [TestMethod]
            public void Cannot_Get_SlaveDevice_By_Invalid_Id()
            {
                // Arrange
                var invalidId = Guid.NewGuid(); // Generate a new GUID that does not exist in the database

                // Act
                var result = _slaveDeviceRepository.GetById(invalidId);

                // Assert
                Assert.IsNull(result); // The result should be null since the ID is invalid
            }

            [TestMethod]
            public void Update_ShouldModifySlaveDevice()
            {
                // Arrange
                var deviceId = Guid.NewGuid();
                var ipAddress = new IP(192, 168, 1, 100);
                var slaveDevice = new SlaveDevice(deviceId, ipAddress, new List<Variable>());
                _context.Set<SlaveDevice>().Add(slaveDevice);
                _unitOfWork.SaveChanges();

                // Act
                var updatedDevice = new SlaveDevice(deviceId, new IP(192, 168, 1, 101), new List<Variable>());
                _slaveDeviceRepository.Update(updatedDevice);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<SlaveDevice>().Find(deviceId);
                Assert.AreEqual("192.168.1.101", result.IpAddress.ToString());
            }

            [TestMethod]
            public void Delete_ShouldRemoveSlaveDevice()
            {
                // Arrange
                var deviceId = Guid.NewGuid();
                var ipAddress = new IP(192, 168, 1, 100);
                var slaveDevice = new SlaveDevice(deviceId, ipAddress, new List<Variable>());
                _context.Set<SlaveDevice>().Add(slaveDevice);
                _unitOfWork.SaveChanges();

                // Act
                _slaveDeviceRepository.Delete(deviceId);
                _unitOfWork.SaveChanges();

                // Assert
                var result = _context.Set<SlaveDevice>().Find(deviceId);
                Assert.IsNull(result); ;
        }
        } } 
