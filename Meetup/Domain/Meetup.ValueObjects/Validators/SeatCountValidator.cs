
using Meetup.ValueObjects.Base;

namespace Meetup.ValueObjects.Validators;

public class SeatCountValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Количество мест не может быть отрицательным");
    }
}
