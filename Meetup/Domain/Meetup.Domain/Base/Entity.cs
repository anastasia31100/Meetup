
namespace Meetup.Domain.Base;

public abstract class Entity<TId>(TId id) where TId : struct, IEquatable<TId>
{
    public TId Id { get; } = id;

    protected Entity() : this(default!) { }

    public override bool Equals(object? other)
    {
        if (other is not Entity<TId> otherEntity)
            return false;

        if (ReferenceEquals(this, otherEntity))
            return true;

        if (GetType() != otherEntity.GetType())
            return false;

        return Id.Equals(otherEntity.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);
}