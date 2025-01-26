using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.ValueObjects;

namespace ModbusData.DataAccess.FluentConfigurations.ModbusNetworks
{
    /// <summary>
    /// Configures the ModbusNetwork entity for Entity Framework Core.
    /// </summary>
    public class ModbusNetworkEntityTypeConfiguration : EntityTypeConfigurationBase<ModbusNetwork>
    {
        /// <summary>
        /// Configures the ModbusNetwork entity type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public override void Configure(EntityTypeBuilder<ModbusNetwork> builder)
        {
            builder.ToTable("ModbusNetworks"); // Consistencia en plural

            base.Configure(builder);

           

            // Configure the MasterIpAddress property
            builder.Property(x => x.MasterIpAddress)
                   .HasConversion(
                       ip => ip.ToString(),
                       ipStr => IP.Parse(ipStr))
                   .HasColumnName("MasterIpAddress")
                   .IsRequired(); // Assuming MasterIpAddress is required
        }
    }
}