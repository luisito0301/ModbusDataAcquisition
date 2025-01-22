using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.FluentConfigurations.Samples
{
    public class SampleEntityTypeConfiguration : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.ToTable("Samples");

            // Definir la clave compuesta
            builder.HasKey(s => new { s.Date, s.VariableId });

            builder.Property(s => s.Date).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Value).IsRequired();
        }
    }
}
