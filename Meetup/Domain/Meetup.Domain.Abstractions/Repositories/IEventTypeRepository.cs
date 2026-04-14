using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с типами мероприятий
/// </summary>
public interface IEventTypeRepository : IRepository<EventType, Guid>
{
    Task<EventType?> GetByNameAsync(EventTypeName name, CancellationToken cancellationToken = default);
}