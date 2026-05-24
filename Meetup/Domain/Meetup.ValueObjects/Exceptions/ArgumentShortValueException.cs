using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.ValueObjects.Exceptions;

public class ArgumentShortValueException(string paramName, string value, int minLength)
    : FormatException($"У параметра \"{paramName}\" длина '{value}' меньше минимально допустимой длины {minLength}");
