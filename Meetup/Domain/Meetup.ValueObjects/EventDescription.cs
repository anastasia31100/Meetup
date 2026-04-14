using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents event description.
/// </summary>
public class EventDescription : ValueObject<string?>
{
    protected EventDescription() : base(new DescriptionValidator(), default(string)) { }

    public EventDescription(string? description) : base(new DescriptionValidator(), description) { }
}