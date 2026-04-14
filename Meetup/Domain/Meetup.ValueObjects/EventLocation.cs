using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents event location.
/// </summary>
public class EventLocation : ValueObject<string>
{
    protected EventLocation() : base(new LocationValidator(), default(string)) { }

    public EventLocation(string location) : base(new LocationValidator(), location) { }
}