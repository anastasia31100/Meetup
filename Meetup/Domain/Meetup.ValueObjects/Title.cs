using Meetup.ValueObjects.Base;
using Meetup.ValueObjects.Validators;

namespace Meetup.ValueObjects;

public class Title(string title) : ValueObject<string>(new TitleValidator(), title);