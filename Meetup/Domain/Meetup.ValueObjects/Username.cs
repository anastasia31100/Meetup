using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

/// <summary>
/// Represents username of the user.
/// </summary>
public class Username : ValueObject<string>
{
    // Конструктор для EF Core (пустой, защищённый)
    protected Username() : base(new UsernameValidator(), default(string)) { }

    // Основной конструктор
    public Username(string name) : base(new UsernameValidator(), name) { }
}