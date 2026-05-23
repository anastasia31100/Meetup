using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meetup.Domain;
using Meetup.ValueObjects;
using Meetup.ValueObjects.Validators;

namespace Meetup.Infrastructure.EntityFramework.Configurations;

public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
{
    public void Configure(EntityTypeBuilder<EventType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new EventTypeName(str))
            .HasMaxLength(EventTypeNameValidator.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(description => description != null ? description.Value : null,
                          str => str != null ? new EventDescription(str) : null)
            .HasMaxLength(DescriptionValidator.MAX_LENGTH);

        builder.HasMany<Event>("_events")
            .WithOne(x => x.EventType)
            .HasForeignKey("EventTypeId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Events);

        // Уникальный индекс на Name
        builder.HasIndex(x => x.Name).IsUnique();
    }
}