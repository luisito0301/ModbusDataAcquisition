using ModbusData.GrpcProtos; // Ensure you have the correct namespace for your gRPC Protos
using Grpc.Net.Client;
using System;
using System.Diagnostics.Metrics;
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

            if (createUnitResponse is null)
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
                Type = VariableType.Analogic, // Adjust as necessary
                IsMeasurement = true,
                Code = "TV001",
                SamplingPeriod = "00:00:01", // Example TimeSpan
                ModbusAddress = 123,
                Value = 10.0,
                Unitid = createUnitResponse.Id // Asignar el ID de la unidad creada
            });

            if (createResponse is null)
            {
                Console.WriteLine("Cannot create analogic variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable analógica. ID: {createResponse.Id}");
            }

            // Resto del código para obtener, modificar y eliminar la variable analógica...
            // (El código que ya tenías para obtener, modificar y eliminar la variable analógica)

            channel.Dispose();
        }
    }
}