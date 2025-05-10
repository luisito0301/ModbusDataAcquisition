using ModbusData.Application;
using ModbusData.Contract;
using ModbusData.Contract.Units;
using ModbusData.Contract.Variables;
using ModbusData.DataAccess;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Units;
using ModbusData.DataAccess.Repositories.Variables;
using ModbusData.DataAccess.Repositories.Devices; 
using ModbusData.GrpcProtos;
using ModbusData.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;
using ModbusData.Contract.Devices;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.DataAccess.Repositories;
using ModbusData.DataAccess.Repositories.ModbusNetworks;

namespace ModbusData.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton("Data Source=Data.sqlite");
            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IVariableRepository<>), typeof(VariableRepository<>));
            builder.Services.AddScoped(typeof(IUnitRepository<>), typeof(UnitRepository<>));
            builder.Services.AddScoped(typeof(IDeviceRepository<>), typeof(DeviceRepository<>)); // Registramos el repositorio de SlaveDevice
            builder.Services.AddScoped(typeof(IModbusNetworkRepository<>), typeof(ModbusNetworkRepository<>)); // Registramos el repositorio de ModbusNetwork
            builder.Services.AddScoped(typeof(ISampleRepository), typeof(SampleRepository)); // Registramos el repositorio de Sample
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddMediatR(new MediatRServiceConfiguration()
            {
                AutoRegisterRequestProcessors = true,
            }
            .RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));

            builder.Logging.AddConsole(); // Esto permite que los logs se muestren en la consola
            builder.Logging.AddDebug(); // Esto permite que los logs se muestren en la ventana de salida de Visual Studio

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<AnalogicVariableService>();
            app.MapGrpcService<DigitalVariableService>();
            app.MapGrpcService<UnitService>();
            app.MapGrpcService<Services.SlaveDeviceService>(); // Mapeo del servicio SlaveDeviceService
            app.MapGrpcService<Services.ModbusNetworkService>(); // Mapeo del servicio ModbusNetworkService
            app.MapGrpcService<Services.SampleService>(); // Mapeo del servicio SampleService

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
