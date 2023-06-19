namespace TimetablePlanning.Utilities.Extensions;
public static class TimeSpanExtensions
{
    public static TimeSpan Max(this TimeSpan time, TimeSpan other ) => time > other ? time : other;
    public static TimeSpan Min(this TimeSpan time, TimeSpan other) => time < other ? time : other;

}
