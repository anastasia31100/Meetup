using Meetup.Domain.Entities;

namespace Meetup.Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с регистрациями
/// </summary>
public interface IRegistrationRepository : IRepository<Registration, Guid>
{
    Task<Registration?> GetByEventAndAttendeeAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Registration>> GetRegistrationsByEventAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Registration>> GetRegistrationsByAttendeeAsync(Guid attendeeId, CancellationToken cancellationToken = default);
    Task<bool> IsAttendeeRegisteredAsync(Guid eventId, Guid attendeeId, CancellationToken cancellationToken = default);
    Task<int> GetActiveRegistrationsCountAsync(Guid eventId, CancellationToken cancellationToken = default);
}