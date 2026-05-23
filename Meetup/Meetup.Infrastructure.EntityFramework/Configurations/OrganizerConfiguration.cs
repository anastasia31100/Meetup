using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Meetup.Domain;
using Meetup.ValueObjects;
using Meetup.ValueObjects.Validators;
namespace Meetup.Infrastructure.EntityFramework.Configurations;

public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasConversion(username => username.Value, str => new Username(str))
            .HasMaxLength(UsernameValidator.MAX_LENGTH);

        builder.Property(x => x.EntityType)
            .IsRequired()
            .HasConversion(entityType => entityType.Value, str => new EntityType(str));
            

        builder.Property(x => x.CompanyName)
            .IsRequired(false)
            .HasConversion(companyName => companyName != null ? companyName.Value : null,
                          str => str != null ? new CompanyName(str) : null)
            .HasMaxLength(CompanyNameValidator.MAX_LENGTH);

        builder.HasMany<Event>("_events")
            .WithOne(x => x.Organizer)
            .HasForeignKey("OrganizerId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Events);
    }
}
