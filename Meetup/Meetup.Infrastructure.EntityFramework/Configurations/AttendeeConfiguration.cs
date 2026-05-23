using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meetup.Domain;
using Meetup.ValueObjects;
using Meetup.ValueObjects.Validators;

namespace Meetup.Infrastructure.EntityFramework.Configurations;

public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasConversion(username => username.Value, str => new Username(str))
            .HasMaxLength(UsernameValidator.MAX_LENGTH);

        builder.HasMany<Registration>("_registrations")
            .WithOne(x => x.Attendee)
            .HasForeignKey("AttendeeId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Registrations);
        builder.Ignore(x => x.ActiveRegistrations);
        builder.Ignore(x => x.History);
    }
}
