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
        static async Task Main(string[] args)
        {
            if (File.Exists("modbusdata.db"))
                File.Delete("modbusdata.db");

            string connectionString = "Data Source=modbusdata.db";

            ApplicationContext context = new ApplicationContext(connectionString);

            

            IUnitOfWork unitOfWork = new UnitOfWork(context);
            IVariableRepository variableRepository = new VariableRepository(context);
            IDeviceRepository deviceRepository = new DeviceRepository(context);
            IUnitRepository unitRepository = new UnitRepository(context);
            Guid Id= Guid.NewGuid();
            Guid Id2= Guid.NewGuid();
            Guid Id3 = Guid.NewGuid();
            AnalogicVariable variable1 = new AnalogicVariable(
                Id, "Temperature", VariableType.Analogic, true, "Temp", TimeSpan.FromSeconds(5), 100);
            AnalogicVariable variable2 = new AnalogicVariable(
                Id2, "Pressure", VariableType.Analogic, true, "Pressure", TimeSpan.FromSeconds(10), 101);
            SlaveDevice device = new SlaveDevice(Id3, new IP(192, 168, 1, 100), new List<Variable> { variable1, variable2 });
            Unit unit = new(Id3, "Main Unit Manufacturer", "MU001", "Factory Floor", new List<Variable> { variable1, variable2 });



            variableRepository.AddVariable(variable1);
            variableRepository.AddVariable(variable2);
            deviceRepository.AddDevice(device);
            unitRepository.AddUnit(unit);
            unitOfWork.SaveChanges();
          

            var variables = variableRepository.GetAllVariables<AnalogicVariable>().ToList();
            var devices = deviceRepository.GetAllDevices().ToList();
            var units = unitRepository.GetAllUnits().ToList();

            foreach (var variable in variables)
                Console.WriteLine($"Variable: {variable.Name}, Type: {variable.Type}, Address: {variable.ModbusAddress}");

            foreach (var dev in devices)
                Console.WriteLine($"Device:  IP: {dev.IpAddress}");

            foreach (var u in units)
                Console.WriteLine($"Unit: {u.ManufactererName}, Area: {u.AreaName}");

            unit.AreaName = "Warehouse";
            unitRepository.UpdateUnit(unit);
            unitOfWork.SaveChanges();

            Unit? modifiedUnit = unitRepository.GetUnitById(unit.Id);
            if (modifiedUnit is null)
                Console.WriteLine("No se pudo obtener la unidad recién modificada.");
            else
                Console.WriteLine($"Nueva ubicación de la unidad: {modifiedUnit.AreaName}");

            deviceRepository.DeleteDevice(device);
            unitOfWork.SaveChanges();

            SlaveDevice? deletedDevice = deviceRepository.GetDeviceById(device.Id);
            if (deletedDevice is null)
                Console.WriteLine("Dispositivo eliminado exitosamente.");
        }
    }
}

