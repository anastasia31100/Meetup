using Meetup.ValueObjects.Validators;
using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects;

public class EntityType : ValueObject<string>
{
    public static readonly EntityType Individual = new("individual");
    public static readonly EntityType Company = new("company");

    public EntityType(string value) : base(new EntityTypeValidator(), value)
    {
    }
}
