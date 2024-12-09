using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.FluentConfigurations.Devices
{
    public class SlaveDeviceEntityTypeConfiguration : EntityTypeConfigurationBase<SlaveDevice>
    {
        public override void Configure(EntityTypeBuilder<SlaveDevice> builder)
        {
            base.Configure(builder); // Llama a la configuración base

            builder.ToTable("SlaveDevices");

            builder.Property(x => x.IpAddress)
                   .HasConversion(
                       ip => ip.ToString(),
                       ipStr => IP.Parse(ipStr))
                   .HasColumnName("IpAddress");

            builder.HasMany(x => x.Variables)
                   .WithOne()
                   .HasForeignKey(x => x.Id);
        }
    }

}
