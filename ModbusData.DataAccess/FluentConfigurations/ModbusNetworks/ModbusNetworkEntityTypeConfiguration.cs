using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.ValueObjects;
using System;

namespace ModbusData.DataAccess.FluentConfigurations.ModbusNetworks
{
    public class ModbusNetworkEntityTypeConfiguration : EntityTypeConfigurationBase<ModbusNetwork>
    {
        public override void Configure(EntityTypeBuilder<ModbusNetwork> builder)
        {
            builder.ToTable("ModbusNetworks"); // Consistencia en plural

            base.Configure(builder);

            builder.HasMany(x => x.Slaves)
                   .WithOne()
                   .HasForeignKey(x => x.Id);

            builder.Property(x => x.MasterIpAddress)
                   .HasConversion(
                       ip => ip.ToString(),
                       ipStr => IP.Parse(ipStr))
                   .HasColumnName("MasterIpAddress");
        }
    }
}
