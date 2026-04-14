using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с организаторами
/// </summary>
public interface IOrganizerRepository : IRepository<Organizer, Guid>
{
    Task<Organizer?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Event>> GetOrganizerEventsAsync(Guid organizerId, CancellationToken cancellationToken = default);
}