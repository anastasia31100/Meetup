using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents maximum number of attendees.
/// </summary>
public class MaxAttendees : ValueObject<int>
{
    protected MaxAttendees() : base(new MaxAttendeesValidator(), default(int)) { }

    public MaxAttendees(int max) : base(new MaxAttendeesValidator(), max) { }
}