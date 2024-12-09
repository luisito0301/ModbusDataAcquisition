using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.Entities.Modbus_Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.FluentConfigurations.ModbusNetworks
{
    public class ModbusNetworkEntityTypeConfiguration : EntityTypeConfigurationBase<ModbusNetwork>
    {
        public override void Configure(EntityTypeBuilder<ModbusNetwork> builder)
        {
            //NICE
            builder.ToTable("ModbusNetwork");
            base.Configure(builder);
            builder.HasMany(x => x.Slaves)
                .WithOne().HasForeignKey(x => x.Id);
            builder.OwnsOne(x => x.MasterIpAddress);
        }
    }
}

