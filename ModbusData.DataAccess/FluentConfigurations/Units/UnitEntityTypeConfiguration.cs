using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.DataAccess.FluentConfigurations.Common;
using ModbusData.Domain.Entities.Unit;

namespace ModbusData.DataAccess.FluentConfigurations.Units
{
    /// <summary>
    /// Configures the Unit entity for Entity Framework Core.
    /// </summary>
    public class UnitEntityTypeConfiguration : EntityTypeConfigurationBase<Unit>
    {
        /// <summary>
        /// Configures the Unit entity type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public override void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units"); // Use plural for consistency
            base.Configure(builder);

            // Configure the relationship with Variables
           
        }
    }
}