using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.FluentConfigurations.Devices
{
    public class SlaveDeviceEntityTypeConfigurationBase : EntityTypeConfigurationBase<SlaveDevice>
    {
        public override void Configure(EntityTypeBuilder<SlaveDevice> builder)
        {
            //MOC
            builder.ToTable("SlaveDevices");
            base.Configure(builder);
            builder.HasMany(x => x.Variables)
                .WithOne().HasForeignKey(x => x.Id);
        }
    }
}
