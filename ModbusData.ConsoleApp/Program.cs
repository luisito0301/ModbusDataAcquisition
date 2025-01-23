using ModbusData.GrpcProtos; // Asegúrate de tener el namespace correcto para tus gRPC Protos
using Grpc.Net.Client;
using System;
using Google.Protobuf.WellKnownTypes;

namespace ModbusData.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presione una tecla para conectar");
            Console.ReadKey();

            Console.WriteLine("Creating channel and client");

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(
                "http://localhost:5051",
                new GrpcChannelOptions { HttpHandler = httpHandler });

            if (channel is null)
            {
                Console.WriteLine("Cannot connect");
                channel.Dispose();
                return;
            }

            var unitClient = new ModbusData.GrpcProtos.Unit.UnitClient(channel);
            var variableClient = new ModbusData.GrpcProtos.AnalogicVariable.AnalogicVariableClient(channel);
            var variableClient2 = new ModbusData.GrpcProtos.DigitalVariable.DigitalVariableClient(channel);
            var slaveDeviceClient = new ModbusData.GrpcProtos.SlaveDeviceService.SlaveDeviceServiceClient(channel);
            var modbusNetworkClient = new ModbusData.GrpcProtos.ModbusNetworkService.ModbusNetworkServiceClient(channel);

            // Crear una unidad
            Console.WriteLine("Presione una tecla para crear una unidad");
            Console.ReadKey();

            var createUnitResponse = unitClient.CreateUnit(new CreateUnitRequest()
            {
                ManufactererName = "Test Manufacturer",
                Code = "TU001",
                AreaName = "Test Area",
                Variables = { } // Puedes dejarlo vacío o agregar variables si es necesario
            });

            if (createUnitResponse == null)
            {
                Console.WriteLine("Cannot create unit");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación de unidad exitosa. ID: {createUnitResponse.Id}");
            }

            // Crear una variable analógica y asignar el ID de la unidad
            Console.WriteLine("Presione una tecla para crear una variable analógica");
            Console.ReadKey();

            var createResponse = variableClient.CreateAnalogicVariable(new CreateAnalogicVariableRequest()
            {
                Name = "Test Variable",
                Type = VariableType.Analogic, // Ajustar según sea necesario
                IsMeasurement = true,
                Code = "TV001",
                SamplingPeriod = "00:00:01", // Ejemplo de TimeSpan
                ModbusAddress = 123,
                Unitid = createUnitResponse.Id // Asignar el ID de la unidad creada
            });

            if (createResponse == null)
            {
                Console.WriteLine("Cannot create analogic variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable analógica. ID: {createResponse.Id}");
            }

            // Crear una variable digital y asignar el ID de la unidad
            Console.WriteLine("Presione una tecla para crear una variable digital");
            Console.ReadKey();

            var createResponse2 = variableClient2.CreateDigitalVariable(new CreateDigitalVariableRequest()
            {
                Name = "Test Variable",
                Type = VariableType.Digital, // Ajustar según sea necesario
                IsMeasurement = true,
                Code = "TV002",
                SamplingPeriod = "00:00:01", // Ejemplo de TimeSpan
                ModbusAddress = 123,
                Unitid = createUnitResponse.Id // Asignar el ID de la unidad creada
            });

            if (createResponse2 == null)
            {
                Console.WriteLine("Cannot create digital variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable digital. ID: {createResponse2.Id}");
            }

            // Crear un dispositivo esclavo
            Console.WriteLine("Presione una tecla para crear un dispositivo esclavo");
            Console.ReadKey();

            var createSlaveDeviceRequest = new CreateSlaveDeviceRequest()
            {
                
                IpAddress = "192.168.1.2" // Ejemplo de IP
            };
            createSlaveDeviceRequest.Variables.Add(new Variable { }); // Agregar variable analógica
            createSlaveDeviceRequest.Variables.Add(new Variable { }); // Agregar variable digital

            var createSlaveDeviceResponse = slaveDeviceClient.CreateSlaveDevice(createSlaveDeviceRequest);

            if (createSlaveDeviceResponse == null)
            {
                Console.WriteLine("Cannot create slave device");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa del dispositivo esclavo. ID: {createSlaveDeviceResponse.Id}");
            }

            // Crear una red Modbus
            Console.WriteLine("Presione una tecla para crear una red Modbus");
            Console.ReadKey();

            var createModbusNetworkResponse = modbusNetworkClient.CreateModbusNetwork(new CreateModbusNetworkRequest()
            {
                MasterIpAddress = "192.168.1.100", // Ejemplo de IP
                Slaves = { createSlaveDeviceResponse.Id } // Ejemplo de dispositivos esclavos
            });

            if (createModbusNetworkResponse == null)
            {
                Console.WriteLine("Cannot create Modbus network");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la red Modbus. ID: {createModbusNetworkResponse.Id}");
            }

            channel.Dispose();
            Console.WriteLine("Pruebas de integración finalizadas.");
        }
    }
}
