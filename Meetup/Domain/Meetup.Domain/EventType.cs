
using Meetup.Domain.Base;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;

namespace Meetup.Domain;

public class EventType : Entity<Guid>
{
    private readonly List<Event> _events = [];

    public EventTypeName Name { get; private set; }
    public EventDescription? Description { get; private set; }
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    protected EventType() { }

    public EventType(Guid id, EventTypeName name, EventDescription? description = null) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Description = description;
    }

    public bool UpdateDetails(EventTypeName newName, EventDescription? newDescription)
    {
        if (newName == null) throw new ArgumentNullValueException(nameof(newName));

        bool updated = false;

        if (!Name.Equals(newName))
        {
            Name = newName;
            updated = true;
        }

        if (!Equals(Description, newDescription))
        {
            Description = newDescription;
            updated = true;
        }

        return updated;
    }
}