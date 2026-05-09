
using Meetup.Domain.Repositories.Abstractions.Base;

namespace Meetup.Domain.Repositories.Abstractions;

public interface IEventTypeRepository : IRepository<EventType, Guid>
{
    Task<EventType?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
