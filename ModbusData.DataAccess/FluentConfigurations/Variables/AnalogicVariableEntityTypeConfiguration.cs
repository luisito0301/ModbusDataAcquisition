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
    public class AnalogicVariableEntityTypeConfiguration : IEntityTypeConfiguration<AnalogicVariable>
    {
        public  void Configure(EntityTypeBuilder<AnalogicVariable> builder)
        {

            builder.ToTable("AnalogicVariables");
            builder.HasBaseType(typeof(Variable));
            builder.Property(x => x.Value)
                   .HasPrecision(18, 2); // Configurar precisión para dos cifras decimales
        }
    }





}
