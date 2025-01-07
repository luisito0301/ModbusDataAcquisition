using ModbusData.GrpcProtos; // Ensure you have the correct namespace for your gRPC Protos
using Grpc.Net.Client;
using System;
using System.Diagnostics.Metrics;
using ModbusData.DataAccess.Contexts;
using ModbusData.Domain.Entities.Unit;

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

            var client = new ModbusData.GrpcProtos.AnalogicVariable.AnalogicVariableClient(channel);

            Console.WriteLine("Presione una tecla para crear una variable analógica");
            Console.ReadKey();

         
            var createResponse = client.CreateAnalogicVariable(new CreateAnalogicVariableRequest()
            {
                Name = "Test Variable",
                Type = VariableType.Analogic, // Adjust as necessary
                IsMeasurement = true,
                Code = "TV001",
                SamplingPeriod = "00:00:01", // Example TimeSpan
                ModbusAddress = 123,
                Value = 10.0,
                Unitid = Guid.NewGuid().ToString()
            });
          
            if (createResponse is null)
            {
                Console.WriteLine("Cannot create analogic variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa. ID: {createResponse.Id}");
            }

            Console.WriteLine("Presione una tecla para obtener todas las variables analógicas");
            Console.ReadKey();
            var getResponse = client.GetAllAnalogicVariables(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Cannot get analogic variables");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Obtención exitosa de {getResponse.Items.Count} variables analógicas");
            }

            Console.WriteLine($"Presione una tecla para obtener la variable analógica con Id {createResponse.Id}");
            Console.ReadKey();
            var getByIdResponse = client.GetAnalogicVariable(new GetRequest() { Id = createResponse.Id.ToString() });
            if (getByIdResponse is null)
            {
                Console.WriteLine("Cannot get analogic variable");
                channel.Dispose();
                return;
            }
            else
            {
                Console.WriteLine($"Obtención exitosa de la variable analógica: {getByIdResponse.AnalogicVariable.Name}");
            }

            Console.WriteLine("Presione una tecla para modificar la variable analógica");
            Console.ReadKey();
            createResponse.Value = 20.0; // Update the value or any other property
            client.UpdateAnalogicVariable(createResponse);

            var updatedGetResponse = client.GetAnalogicVariable(new GetRequest() { Id = createResponse.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.AnalogicVariable.Value == createResponse.Value)
            {
                Console.WriteLine($"Modificación exitosa.");
            }

            Console.WriteLine("Presione una tecla para eliminar la variable analógica");
            Console.ReadKey();

            client.DeleteAnalogicVariable(new DeleteRequest() { Id = createResponse.Id });
            var deletedGetResponse = client.GetAnalogicVariable(new GetRequest() { Id = createResponse.Id });
            if (deletedGetResponse is null)
            {
                Console.WriteLine($"Eliminación exitosa.");
            }

            channel.Dispose();
        }
    }
}