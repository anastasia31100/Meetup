using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents event type name.
/// </summary>
public class EventTypeName : ValueObject<string>
{
    protected EventTypeName() : base(new EventTypeNameValidator(), default(string)) { }

    public EventTypeName(string name) : base(new EventTypeNameValidator(), name) { }
}