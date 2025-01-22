using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModbusData.Domain.Common;

namespace ModbusData.DataAccess.FluentConfigurations.Common
{
    /// <summary>
    /// Base class for configuring entities in Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        /// <summary>
        /// Configures the entity type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Configure the primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
        }
    }
}