using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meetup.Domain;
namespace Meetup.Infrastructure.EntityFramework.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.RegisteredAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.Property(x => x.IsCancelled).IsRequired();

        builder.HasOne(x => x.Event)
            .WithMany("_registrations")
            .HasForeignKey("EventId")
            .HasPrincipalKey(x => x.Id);

        builder.HasOne(x => x.Attendee)
            .WithMany("_registrations")
            .HasForeignKey("AttendeeId")
            .HasPrincipalKey(x => x.Id);

        // Уникальный индекс для предотвращения дублирования регистраций
        builder.HasIndex("EventId", "AttendeeId").IsUnique();

        // Индекс для поиска активных регистраций
        builder.HasIndex(x => x.IsCancelled);
    }
}