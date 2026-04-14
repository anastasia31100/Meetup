using Meetup.ValueObjects;

namespace Meetup.Domain.Entities;

public class Organizer
{
    public Guid Id { get; private set; }
    public Username Username { get; private set; }
    public EntityType EntityType { get; private set; }
    public CompanyName CompanyName { get; private set; }

    // Конструктор для EF Core (пустой, с protected)
    protected Organizer() { }

    // Основной конструктор
    public Organizer(Username username, EntityType entityType, CompanyName companyName)
    {
        Id = Guid.NewGuid();
        Username = username ?? throw new ArgumentNullException(nameof(username));
        EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
    }
}