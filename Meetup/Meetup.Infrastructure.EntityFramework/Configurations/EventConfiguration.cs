using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meetup.Domain;
using Meetup.ValueObjects;
using Meetup.ValueObjects.Validators;
namespace Meetup.Infrastructure.EntityFramework.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasConversion(title => title.Value, str => new Title(str))
            .HasMaxLength(TitleValidator.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(description => description != null ? description.Value : null,
                          str => str != null ? new EventDescription(str) : null);

        builder.Property(x => x.EventDate)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.Property(x => x.Location)
            .IsRequired()
            .HasConversion(location => location.Value, str => new Location(str))
            .HasMaxLength(LocationValidator.MAX_LENGTH);

        builder.Property(x => x.MaxAttendees)
            .IsRequired()
            .HasConversion(seatCount => seatCount.Value, value => new SeatCount(value));

        builder.Property(x => x.CurrentAttendees)
            .IsRequired()
            .HasConversion(seatCount => seatCount.Value, value => new SeatCount(value));

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.IsCancelled).IsRequired();

        builder.HasOne(x => x.Organizer)
            .WithMany("_events")
            .HasForeignKey("OrganizerId")
            .HasPrincipalKey(x => x.Id);

        builder.HasOne(x => x.EventType)
            .WithMany("_events")
            .HasForeignKey("EventTypeId")
            .HasPrincipalKey(x => x.Id);

        builder.HasMany<Registration>("_registrations")
            .WithOne(x => x.Event)
            .HasForeignKey("EventId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Registrations);

        // Индексы для часто используемых запросов
        builder.HasIndex(x => x.EventDate);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsCancelled);
        builder.HasIndex(new[] { "OrganizerId", "EventDate" });
    }
}
