using Meetup.ValueObjects;

namespace Meetup.Domain.Entities;

public class Attendee
{
    public Guid Id { get; private set; }
    public Username Username { get; private set; }

    protected Attendee() { }

    public Attendee(Username username)
    {
        Id = Guid.NewGuid();
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}