using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents event date.
/// </summary>
public class EventDate : ValueObject<DateTime>
{
    protected EventDate() : base(new EventDateValidator(), default(DateTime)) { }

    public EventDate(DateTime date) : base(new EventDateValidator(), date) { }
}