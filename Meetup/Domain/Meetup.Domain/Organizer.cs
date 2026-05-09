using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;
namespace Meetup.Domain;

public class Organizer : Entity<Guid>
{
    private readonly List<Event> _events = [];

    public Username Username { get; private set; }
    public string EntityType { get; private set; }
    public CompanyName? CompanyName { get; private set; }
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    protected Organizer() { }

    public Organizer(Guid id, Username username, string entityType, CompanyName? companyName = null) : base(id)
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));

        if (string.IsNullOrWhiteSpace(entityType))
            throw new ArgumentNullValueException(nameof(entityType));

        if (entityType != "individual" && entityType != "company")
            throw new InvalidEntityTypeException(entityType);

        EntityType = entityType;
        CompanyName = entityType == "company" ? companyName : null;
    }

    public Event CreateEvent(Title title, EventDescription description, DateTime eventDate,
        Location location, int maxAttendees, EventType eventType)
    {
        var newEvent = new Event(Guid.NewGuid(), this, eventType, title, description,
            eventDate, location, maxAttendees);
        _events.Add(newEvent);
        return newEvent;
    }

    public bool EditEvent(Event eventToEdit, Title newTitle, EventDescription newDescription,
        DateTime newEventDate, Location newLocation, int newMaxAttendees)
    {
        if (eventToEdit.Organizer != this)
            throw new AnotherOrganizerEditEventException(eventToEdit, this);

        if (!_events.Contains(eventToEdit))
            throw new EventNotBelongOrganizerException(eventToEdit, this);

        if (eventToEdit.Started())
            throw new EventAlreadyStartedException(eventToEdit);

        return eventToEdit.UpdateDetails(newTitle, newDescription, newEventDate, newLocation, newMaxAttendees);
    }

    public void CancelEvent(Event eventToCancel)
    {
        if (eventToCancel.Organizer != this)
            throw new AnotherOrganizerCancelEventException(eventToCancel, this);

        if (!_events.Contains(eventToCancel))
            throw new EventNotBelongOrganizerException(eventToCancel, this);

        if (eventToCancel.Started())
            throw new EventAlreadyStartedException(eventToCancel);

        eventToCancel.Cancel();
    }

    public bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username.Equals(newUsername)) return false;
        Username = newUsername;
        return true;
    }
}