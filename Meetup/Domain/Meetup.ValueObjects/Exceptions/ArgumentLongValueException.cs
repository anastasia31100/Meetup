using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.ValueObjects.Exceptions;

public class ArgumentLongValueException(string paramName, string value, int maxLength)
    : FormatException($"У параметра \"{paramName}\" длина '{value}' больше максимально допустимой длины {maxLength}");
