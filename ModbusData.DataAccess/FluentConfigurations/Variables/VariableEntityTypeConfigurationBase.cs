using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Unit;
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
            base.Configure(builder); // Llama a la configuración base

            // Configuración para SamplingPeriod
            builder.Property(x => x.SamplingPeriod)
                   .HasConversion(
                       v => v.ToString(),
                       v => TimeSpan.Parse(v))
                   .IsRequired();
            // Configure the UnitId property as a foreign key
            builder.Property(x => x.UnitId)
                   .IsRequired(); // Assuming UnitId is required

            // Configure the foreign key relationship without a navigation property
            builder.HasOne<Unit>() // Specify the Unit entity
                   .WithMany() // No navigation property in Unit
                   .HasForeignKey(x => x.UnitId); // Set the foreign key
        }
    }


}
