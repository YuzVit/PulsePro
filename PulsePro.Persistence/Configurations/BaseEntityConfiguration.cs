using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePro.Domain.Entities;

namespace PulsePro.Persistence.Configurations;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // усі наші сутності мають Primary Key Id типу Guid
        builder.HasKey("Id");
    }
}