using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.ValueObjects;

namespace ModbusData.DataAccess.FluentConfigurations.Devices
{
    /// <summary>
    /// Configures the SlaveDevice entity for Entity Framework Core.
    /// </summary>
    public class SlaveDeviceEntityTypeConfiguration : EntityTypeConfigurationBase<SlaveDevice>
    {
        /// <summary>
        /// Configures the SlaveDevice entity type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public override void Configure(EntityTypeBuilder<SlaveDevice> builder)
        {
            base.Configure(builder); // Call the base configuration

            builder.ToTable("SlaveDevices");

            // Configure the IpAddress property
            builder.Property(x => x.IpAddress)
                   .HasConversion(
                       ip => ip.ToString(), // Convert IP to string for storage
                       ipStr => IP.Parse(ipStr)) // Convert string back to IP
                   .HasColumnName("IpAddress")
                   .IsRequired(); // Assuming IpAddress is required

           
        }
    }
}