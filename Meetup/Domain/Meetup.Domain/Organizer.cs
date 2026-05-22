using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;
namespace Meetup.Domain;

public class Organizer : Entity<Guid>
{

    private readonly ICollection<Event> _events = new List<Event>();

    public Username Username { get; private set; }
    public EntityType EntityType { get; private set; } // теперь Value Object
    public CompanyName? CompanyName { get; private set; }
    public IReadOnlyCollection<Event> Events => _events.ToList().AsReadOnly();

    protected Organizer() { }

    protected Organizer(Guid id, Username username, EntityType entityType, CompanyName? companyName = null) : base(id)
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
        EntityType = entityType ?? throw new ArgumentNullValueException(nameof(entityType));

        if (entityType == EntityType.Company && companyName == null)
            throw new ArgumentNullValueException(nameof(companyName), "Для компании необходимо указать название");

        CompanyName = companyName;
    }

    // Публичный конструктор
    public Organizer(Username username, EntityType entityType, CompanyName? companyName = null)
        : this(Guid.NewGuid(), username, entityType, companyName)
    {
    }

    public Event CreateEvent(Title title, EventDescription description, DateTime eventDate,
        Location location, int maxAttendees, EventType eventType)
    {
        var newEvent = new Event(this, eventType, title, description, eventDate, location, maxAttendees);
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

        // Передаём this для проверки в Event
        return eventToEdit.UpdateDetails(this, newTitle, newDescription, newEventDate, newLocation, newMaxAttendees);
    }

    public void CancelEvent(Event eventToCancel)
    {
        if (eventToCancel.Organizer != this)
            throw new AnotherOrganizerCancelEventException(eventToCancel, this);

        if (!_events.Contains(eventToCancel))
            throw new EventNotBelongOrganizerException(eventToCancel, this);

        if (eventToCancel.Started())
            throw new EventAlreadyStartedException(eventToCancel);

        eventToCancel.Cancel(this); // передаём this
    }

    public bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }
}