using Meetup.Domain.Entities;
using Meetup.ValueObjects;

namespace Meetup.Domain.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с участниками
/// </summary>
public interface IAttendeeRepository : IRepository<Attendee, Guid>
{
    Task<Attendee?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Registration>> GetAttendeeRegistrationsAsync(Guid attendeeId, CancellationToken cancellationToken = default);
}