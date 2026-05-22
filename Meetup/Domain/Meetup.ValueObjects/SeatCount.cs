
using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class SeatCount : ValueObject<int>
{
    public SeatCount(int value) : base(new SeatCountValidator(), value)
    {
    }

    public static SeatCount operator +(SeatCount a, SeatCount b) => new(a.Value + b.Value);
    public static SeatCount operator -(SeatCount a, SeatCount b) => new(a.Value - b.Value);
    public static bool operator >(SeatCount a, SeatCount b) => a.Value > b.Value;
    public static bool operator <(SeatCount a, SeatCount b) => a.Value < b.Value;
    public static bool operator >=(SeatCount a, SeatCount b) => a.Value >= b.Value;
    public static bool operator <=(SeatCount a, SeatCount b) => a.Value <= b.Value;
}
