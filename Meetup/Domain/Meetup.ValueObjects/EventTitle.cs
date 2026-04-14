using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents event title.
/// </summary>
public class EventTitle : ValueObject<string>
{
    protected EventTitle() : base(new TitleValidator(), default(string)) { }

    public EventTitle(string title) : base(new TitleValidator(), title) { }
}