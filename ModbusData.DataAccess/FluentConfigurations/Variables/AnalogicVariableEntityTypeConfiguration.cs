using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.Domain.Entities.Unit;
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.DataAccess.FluentConfigurations.Variables
{
    /// <summary>
    /// Configures the AnalogicVariable entity for Entity Framework Core.
    /// </summary>
    public class AnalogicVariableEntityTypeConfiguration : IEntityTypeConfiguration<AnalogicVariable>
    {
        /// <summary>
        /// Configures the AnalogicVariable entity type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<AnalogicVariable> builder)
        {
            builder.ToTable("AnalogicVariables");

            // Configure the base type for TPH inheritance
            builder.HasBaseType<Variable>();

            

           
        }
    }
}
