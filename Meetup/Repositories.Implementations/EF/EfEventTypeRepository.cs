using Microsoft.EntityFrameworkCore;
using Meetup.Domain;
using Meetup.Domain.Repositories.Abstractions;
using Meetup.Infrastructure.EntityFramework;
namespace Repositories.Implementations.EF;

public class EfEventTypeRepository(ApplicationDbContext context)
    : EfRepository<EventType, Guid>(context), IEventTypeRepository
{
    private readonly DbSet<EventType> _eventTypes = context.Set<EventType>();

    public override async Task<EventType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _eventTypes
            .Include("_events")
            .FirstOrDefaultAsync(et => et.Id == id, cancellationToken);

    public async Task<EventType?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await _eventTypes
            .Include("_events")
            .FirstOrDefaultAsync(et => et.Name.Value == name, cancellationToken);
}
