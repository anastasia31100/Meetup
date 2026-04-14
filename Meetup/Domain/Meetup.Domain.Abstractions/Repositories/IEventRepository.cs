using Meetup.Domain.Entities;

namespace Meetup.Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с мероприятиями
/// </summary>
public interface IEventRepository : IRepository<Event, Guid>
{
    Task<IReadOnlyList<Event>> GetUpcomingEventsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(Guid organizerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Event>> GetEventsByTypeAsync(Guid eventTypeId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Event>> SearchByTitleAsync(string searchTerm, CancellationToken cancellationToken = default);
}