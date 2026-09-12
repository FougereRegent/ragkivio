using Ragkivio.Domain.Common;
using Ragkivio.Domain.Common.Exceptions;
using System.Text.RegularExpressions;

namespace Ragkivio.Domain.User;

public sealed class User : Common.Entity
{
    private const string regexName = @"^[\p{L}]+(?:[ '-][\p{L}]+)*$";
    public string Email
    {
        get; private set
        {
            const string emailRegex = @"^[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?(?:\.[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?)+$";
            if (field == value)
                return;

            if (!Regex.IsMatch(value, emailRegex))
                throw new BusinessException(nameof(Email), "");

            field = value;

        }
    } = string.Empty;

    public string FirstName
    {
        get; private set
        {
            if (field == value)
                return;

            if (!Regex.IsMatch(value, regexName))
                throw new Exception();

            field = value;
        }
    } = string.Empty;

    public string LastName
    {
        get; set
        {
            if (field == value)
                return;

            if (!Regex.IsMatch(value, regexName))
                throw new Exception();

            field = value;
        }
    } = string.Empty;
    public string? PhoneNumber
    {
        get; set
        {
            const string phoneNumberRegex = @"^\+?[1-9]\d{7,14}$";
            if (field == value)
                return;

            if (value is null)
                return;

            if (!Regex.IsMatch(value, phoneNumberRegex))
                throw new Exception();
        }
    } = string.Empty;
    public bool IsRegistered { get; private set; } = false;

    public DateOnly BirthDate
    {
        get; set
        {

            if (field.Equals(value))
                return;
            var dateTimeProvider = new DateTimeProvider();
            var nowDateOnly = DateOnly.FromDateTime(dateTimeProvider.Now);

            if (value >= nowDateOnly)
            {
                throw new FutureDateException(nameof(BirthDate), value.ToString());
            }

            field = value;
        }
    } = new DateOnly();

    public Config? Config { get; set; } = null;


    public void RegisterUser(string firstName, string lastName, string? phoneNumber, DateOnly birthDate)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(firstName);
        ArgumentNullException.ThrowIfNullOrEmpty(lastName);

        this.FirstName = firstName;
        this.LastName = lastName;
        this.PhoneNumber = phoneNumber;
        this.BirthDate = birthDate;

        this.IsRegistered = true;
    }
}