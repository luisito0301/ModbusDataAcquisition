using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Data.Sqlite;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.ValueObjects;
using ModbusData.DataAccess.FluentConfigurations.Devices;
using ModbusData.DataAccess.FluentConfigurations.ModbusNetworks;
using ModbusData.DataAccess.FluentConfigurations.Units;
using ModbusData.DataAccess.FluentConfigurations.Variables;

namespace ModbusData.DataAccess.Contexts
{
    /// <summary>
    /// Define la estructura de la base de datos de la aplicación.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        #region Tables

        /// <summary>
        /// Tabla de redes Modbus.
        /// </summary>
        public DbSet<ModbusNetwork> ModbusNetworks { get; set; }
        /// <summary>
        /// Tabla de dispositivos esclavos.
        /// </summary>
        public DbSet<SlaveDevice> SlaveDevices { get; set; }
        /// <summary>
        /// Tabla de variables.
        /// </summary>
        public DbSet<AnalogicVariable> AnalogicVariables { get; set; }
        public DbSet<DigitalVariable> DigitalVariables { get; set; }
        /// <summary>
        /// Tabla de unidades.
        /// </summary>
        public DbSet<Unit> Units { get; set; }

        #endregion

        /// <summary>
        /// Requerido por EntityFrameworkCore para migraciones.
        /// </summary>
        public ApplicationContext() { }

        /// <summary>
        /// Inicializa un objeto <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="connectionString">
        /// Cadena de conexión.
        /// </param>
        public ApplicationContext(string connectionString)
            : base(GetOptions(connectionString))
        {
        }

        /// <summary>
        /// Inicializa un objeto <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="options">
        /// Opciones del contexto.
        /// </param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=modbusdata.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SlaveDeviceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ModbusNetworkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnalogicVariableEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DigitalVariableEntityTypeConfiguration());
        }

        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connectionString).Options;
        }

        #endregion
    }

    /// <summary>
    /// Habilita características en tiempo de diseño de la base de datos de la aplicación.
    /// Ej: Migraciones.
    /// </summary>
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            try
            {
                var connectionString = "Data Source=modbusdata.db";
                optionsBuilder.UseSqlite(connectionString);
            }
            catch (Exception)
            {
                // Manejar error aquí.. significa que la DLL no tiene un archivo de configuración satelital.
                throw;
            }

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
