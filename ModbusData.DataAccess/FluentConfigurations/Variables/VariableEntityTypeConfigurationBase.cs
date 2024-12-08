using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.FluentConfigurations.Variables
{
    public class VariableEntityTypeConfigurationBase : EntityTypeConfigurationBase<Variable>
    {
        public override void Configure(EntityTypeBuilder<Variable> builder)
        {
            builder.ToTable("Variable");
            base.Configure(builder);
            
        }
    }
}
