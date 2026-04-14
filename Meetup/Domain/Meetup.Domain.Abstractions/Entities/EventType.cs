using Meetup.ValueObjects;

namespace Meetup.Domain.Entities;

public class EventType
{
    public Guid Id { get; private set; }
    public EventTypeName Name { get; private set; }
    public string? Description { get; private set; }
    public string? Color { get; private set; }

    protected EventType() { }

    public EventType(EventTypeName name, string? description = null, string? color = null)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Color = color;
    }
}