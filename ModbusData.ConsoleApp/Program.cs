using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Variables;
using ModbusData.DataAccess.Repositories.Devices;
using ModbusData.DataAccess.Repositories.Units;
using ModbusData.DataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ModbusData.Domain.ValueObjects;
using ModbusData.Contract;
using ModbusData.Contract.Variables;
using ModbusData.Contract.Devices;
using ModbusData.Contract.Units;
using ModbusData.DataAccess;
using ModbusData.Domain.Types;

namespace ModbusData.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("modbusdata.db"))
                File.Delete("modbusdata.db");

            string connectionString = "Data Source=modbusdata.db";

            using ApplicationContext context = new ApplicationContext(connectionString);
            context.Database.EnsureCreated(); // Ensure the database is created

            IUnitOfWork unitOfWork = new UnitOfWork(context);
            IVariableRepository variableRepository = new VariableRepository(context);
            IDeviceRepository deviceRepository = new DeviceRepository(context);
            IUnitRepository unitRepository = new UnitRepository(context);

            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            Guid Id3 = Guid.NewGuid();
            Guid Id4 = Guid.NewGuid();

            // Create separate instances for each variable and set the UnitId
            AnalogicVariable variable1 = new AnalogicVariable(
                Id, "Temperature", VariableType.Analogic, true, "Temp", TimeSpan.FromSeconds(5), 100)
            {
                UnitId = Id4 // Set the UnitId to the ID of the associated Unit
            };

            AnalogicVariable variable2 = new AnalogicVariable(
                Id2, "Pressure", VariableType.Analogic, true, "Pressure", TimeSpan.FromSeconds(10), 101)
            {
                UnitId = Id4 // Set the UnitId to the ID of the associated Unit
            };

            // Add variables to the repository first
            variableRepository.AddVariable(variable1);
            variableRepository.AddVariable(variable2);

            // Create new instances of devices and units
            SlaveDevice device = new SlaveDevice(Id3, new IP(192, 168, 1, 100), new List<Variable> { variable1 });
            Unit unit = new(Id4, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable> { variable2 });

            // Add device and unit to the repository
            deviceRepository.AddDevice(device);
            unitRepository.AddUnit(unit);

            // Save changes to the database
            unitOfWork.SaveChanges();

            // Retrieve and display entities
            var variables = variableRepository.GetAllVariables<AnalogicVariable>().ToList();
            var devices = deviceRepository.GetAllDevices().ToList();
            var units = unitRepository.GetAllUnits().ToList();

            foreach (var variable in variables)
                Console.WriteLine($"Variable: {variable.Name}, Type: {variable.Type}, Address: {variable.ModbusAddress}, UnitId: {variable.UnitId}");

            foreach (var dev in devices)
                Console.WriteLine($"Device:  IP: {dev.IpAddress}");

            foreach (var u in units)
                Console.WriteLine($"Unit: {u.ManufactererName}, Area: {u.AreaName}");

            // Update unit area
            unit.AreaName = "Warehouse";
            unitRepository.UpdateUnit(unit);
            unitOfWork.SaveChanges(); // Save changes after updating the unit

            Unit? modifiedUnit = unitRepository.GetUnitById(unit.Id);
            if (modifiedUnit is null)
                Console.WriteLine("No se pudo obtener la unidad recién modificada.");
            else
                Console.WriteLine($"Nueva ubicación de la unidad: {modifiedUnit.AreaName}");

            deviceRepository.DeleteDevice(device);
            unitOfWork.SaveChanges(); // Save changes after deleting the device
        }
    }
}