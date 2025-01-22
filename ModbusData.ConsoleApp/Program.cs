using ModbusData.GrpcProtos; // Asegúrate de que tienes el espacio de nombres correcto para tus gRPC Protos
using Grpc.Net.Client;
using System;

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

            var analogicVariableClient = new AnalogicVariable.AnalogicVariableClient(channel); // Cliente para variables analógicas
            var digitalVariableClient = new DigitalVariable.DigitalVariableClient(channel); // Cliente para variables digitales

            // Crear una variable analógica
            Console.WriteLine("Presione una tecla para crear una variable analógica");
            Console.ReadKey();

            var createAnalogicVariableResponse = analogicVariableClient.CreateAnalogicVariable(new CreateAnalogicVariableRequest()
            {
                Name = "Test Analogic Variable",
                Type = VariableType.Analogic, // Tipo de variable
                IsMeasurement = true,
                Code = "TAV001",
                SamplingPeriod = "00:00:01", // Ejemplo de TimeSpan
                ModbusAddress = 123,
                Value = 10.0,
                Unitid = "123e4567-e89b-12d3-a456-426614174000" // Ejemplo de ID de unidad
            });

            if (createAnalogicVariableResponse is null)
            {
                Console.WriteLine("No se puede crear la variable analógica");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable analógica. ID: {createAnalogicVariableResponse.Base.Id}");
            }

            // Crear una variable digital
            Console.WriteLine("Presione una tecla para crear una variable digital");
            Console.ReadKey();

            var createDigitalVariableResponse = digitalVariableClient.CreateDigitalVariable(new CreateDigitalVariableRequest()
            {
                Name = "Test Digital Variable",
                Type = VariableType.Digital, // Tipo de variable
                IsMeasurement = true,
                Code = "TDV001",
                SamplingPeriod = "00:00:01", // Ejemplo de TimeSpan
                ModbusAddress = 456,
                Value = 1.0,
                Unitid = "123e4567-e89b-12d3-a456-426614174000" // Ejemplo de ID de unidad
            });

            if (createDigitalVariableResponse is null)
            {
                Console.WriteLine("No se puede crear la variable digital");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable digital. ID: {createDigitalVariableResponse.Base.Id}");
            }

            // Resto del código para obtener, modificar y eliminar las variables...

            channel.Dispose();
        }
    }
}
