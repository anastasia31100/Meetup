
using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;

namespace Meetup.Domain;

public class EventType : Entity<Guid>
{
    private readonly ICollection<Event> _events = new List<Event>();

    public EventTypeName Name { get; private set; }
    public EventDescription? Description { get; private set; }
    public IReadOnlyCollection<Event> Events => _events.ToList().AsReadOnly();

    protected EventType() { }

    protected EventType(Guid id, EventTypeName name, EventDescription? description = null) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Description = description;
    }

    public EventType(EventTypeName name, EventDescription? description = null)
        : this(Guid.NewGuid(), name, description)
    {
    }

    public bool UpdateDetails(EventTypeName newName, EventDescription? newDescription)
    {
        if (newName == null) throw new ArgumentNullValueException(nameof(newName));

        bool updated = false;

        if (Name != newName)
        {
            Name = newName;
            updated = true;
        }

        if (Description != newDescription)
        {
            Description = newDescription;
            updated = true;
        }

        return updated;
    }
}