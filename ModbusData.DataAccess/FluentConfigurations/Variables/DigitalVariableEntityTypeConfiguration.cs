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
    public class DigitalVariableEntityTypeConfiguration : IEntityTypeConfiguration<DigitalVariable>
    {
        public  void Configure(EntityTypeBuilder<DigitalVariable> builder)
        {
            

            builder.ToTable("DigitalVariables");
            builder.HasBaseType(typeof(Variable));
            builder.Property(x => x.Value)
                   .HasConversion<short>()
                   .HasColumnName("Value")
                   .HasMaxLength(1024)
                   .IsRequired();
        }
    }


}
