namespace Ragkivio.Domain.Common;

public sealed class DateTimeProvider
{
    private IDateTimeMocker _dateTimeMocker => _asyncLocalDateTimeMocker.Value!;
    private static AsyncLocal<IDateTimeMocker> _asyncLocalDateTimeMocker = new AsyncLocal<IDateTimeMocker>();

    public DateTime Now => _dateTimeMocker?.Now ?? DateTime.Now;

    public DateTimeProvider() { }

    public static void SetDateTimeMocker(IDateTimeMocker _dateTimeMocker) =>
        _asyncLocalDateTimeMocker.Value = _dateTimeMocker;
}