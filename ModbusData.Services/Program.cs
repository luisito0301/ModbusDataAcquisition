using ModbusData.Contract;
using ModbusData.Contract.Variables;
using ModbusData.DataAccess;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Variables;
using System.Reflection.Metadata;
using ModbusData.GrpcProtos;
namespace ModbusData.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddSingleton("Data Source=ModbusDataDB.sqlite");
            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IVariableRepository<>), typeof(VariableRepository<>));

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


            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}


