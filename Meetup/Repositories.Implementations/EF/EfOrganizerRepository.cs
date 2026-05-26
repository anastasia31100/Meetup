using Microsoft.EntityFrameworkCore;
using Meetup.Domain;
using Meetup.Domain.Repositories.Abstractions;
using Meetup.Infrastructure.EntityFramework;
namespace Repositories.Implementations.EF;

public class EfOrganizerRepository(ApplicationDbContext context)
    : EfRepository<Organizer, Guid>(context), IOrganizerRepository
{
    private readonly DbSet<Organizer> _organizers = context.Set<Organizer>();

    public override async Task<Organizer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _organizers
            .Include("_events")
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task<Organizer?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        => await _organizers
            .Include("_events")
            .FirstOrDefaultAsync(o => o.Username.Value == username, cancellationToken);

    public async Task<IEnumerable<Event>> GetOrganizerEventsAsync(Guid organizerId, CancellationToken cancellationToken, bool includeCancelled = false)
    {
        var organizer = await GetByIdAsync(organizerId, cancellationToken);
        if (organizer is null) return Enumerable.Empty<Event>();

        return includeCancelled
            ? organizer.Events
            : organizer.Events.Where(e => !e.IsCancelled);
    }
}