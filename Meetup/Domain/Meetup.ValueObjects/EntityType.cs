using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents entity type (individual, company, sole_proprietor).
/// </summary>
public class EntityType : ValueObject<string>
{
    protected EntityType() : base(new EntityTypeValidator(), default(string)) { }

    public EntityType(string type) : base(new EntityTypeValidator(), type) { }
}