using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Records;
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

            // Configurar la propiedad UnitId como una clave foránea
            builder.Property(x => x.UnitId)
                   .IsRequired(); // Asumimos que UnitId es requerido

            // Configurar la relación de clave foránea sin una propiedad de navegación
            builder.HasOne<Unit>() // Especificar la entidad Unit
                   .WithMany() // No tiene propiedad de navegación en Unit
                   .HasForeignKey(x => x.UnitId); // Establecer la clave foránea

            // Configuración para Sample como un tipo propio (owned type)
            builder.OwnsMany(v => v.Samples, a =>
            {
                a.WithOwner().HasForeignKey(s => s.VariableId);
                a.Property(s => s.Date).IsRequired();
                a.HasKey(s => new { s.VariableId, s.Date }); // Clave compuesta


            });
        }
    }
}

