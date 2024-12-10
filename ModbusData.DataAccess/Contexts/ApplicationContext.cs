using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.DataAccess.FluentConfigurations.Devices;
using ModbusData.DataAccess.FluentConfigurations.ModbusNetworks;
using ModbusData.DataAccess.FluentConfigurations.Units;
using ModbusData.DataAccess.FluentConfigurations.Variables;

namespace ModbusData.DataAccess.Contexts
{
    public class ApplicationContext : DbContext
    {
        #region Tables 
        public DbSet<ModbusNetwork> ModbusNetworks { get; set; }
        public DbSet<SlaveDevice> SlaveDevices { get; set; }
        public DbSet<AnalogicVariable> AnalogicVariables { get; set; }
        public DbSet<DigitalVariable> DigitalVariables { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Unit> Units { get; set; }
        #endregion
        public ApplicationContext()
        {
        }
        public ApplicationContext(string connectionString)
            : base(GetOptions(connectionString))
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SlaveDeviceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ModbusNetworkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnalogicVariableEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DigitalVariableEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VariableEntityTypeConfigurationBase());
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }

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
                throw;
            }

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
