using Meetup.Domain.Entities;
using Meetup.Domain.Abstractions.Repositories;
using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
    private readonly List<EventType> _eventTypes = new();

    public Task<EventType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = _eventTypes.FirstOrDefault(et => et.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<EventType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<EventType>>(_eventTypes.ToList());
    }

    public Task AddAsync(EventType entity, CancellationToken cancellationToken = default)
    {
        _eventTypes.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(EventType entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(EventType entity, CancellationToken cancellationToken = default)
    {
        _eventTypes.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_eventTypes.Any(et => et.Id == id));
    }

    public Task<EventType?> GetByNameAsync(EventTypeName name, CancellationToken cancellationToken = default)
    {
        var result = _eventTypes.FirstOrDefault(et => et.Name == name);
        return Task.FromResult(result);
    }
}