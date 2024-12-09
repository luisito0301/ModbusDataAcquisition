using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.FluentConfigurations.Units
{
    public class UnitEntityTypeConfigurationBase : EntityTypeConfigurationBase<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> builder)
        {
            //nice
            builder.ToTable("Unit");
            base.Configure(builder);
            builder.HasMany(x => x.Variables)
                .WithOne().HasForeignKey(x => x.Id);
        }
    }
}
