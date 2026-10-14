using System.Text.RegularExpressions;
using Ragkivio.Domain.Common;
using Ragkivio.Domain.Common.Exceptions;

namespace Ragkivio.Domain.Project;

public enum Visibility
{
    SHARED,
    PRIVATE,
}

public sealed class Project : Common.Entity
{
    public string Name
    {
        get; set
        {
            const string NameRegex = @"^[\p{L}\p{N}][\p{L}\p{N} _-]{1,48}[\p{L}\p{N}]$";;

            if (field == value)
                return;

            if (!Regex.IsMatch(value, NameRegex))
            {
                throw new BusinessException(nameof(Name), "name was not valid");
            }

            field = value;
        }
    } = string.Empty;

    public string? Description
    {
        get; set
        {
            if (field == value)
                return;

            if (value is null)
            {
                field = value;
                return;
            }

            const int maxSize = 255;
            if (value.Length > 255)
                throw new LengthTooBigException(nameof(Description), maxSize);

            field = value;
        }
    } = null;

    public ColorElement Color { get; set; } = new ColorElement { ColorHex = "#FFFFFF" };

    public Visibility Visibility { get; set; } = Visibility.PRIVATE;
}