namespace TimetablePlanning.Components.Scheduling;

public record SchedlingSettings
{
    public static SchedlingSettings Default => new ();
    public TimeSpan DefaultLeadInTime = TimeSpan.FromMinutes(10);
    public TimeSpan DefaultLeadOutTime = TimeSpan.FromMinutes(20);
}

public record GraphSettings
{
    public static GraphSettings Default => new ();
    public TimeAxisDirection AxisDirection { get; set; }
    public TimeSpan StartTime { get; set; } = TimeSpan.FromHours(6);
    public TimeSpan EndTime { get; set; } = TimeSpan.FromHours(20);
    public Offset TimeAxisSpacing { get; set; } = new(30, 30);
    public Offset KilometerAxisSpacing { get; set; } = new(100, 60);
    public int EndMargin { get; set; } = 20;
    public int TrackSpacing { get; set; } = 8;
    public int MinStationSpacing { get; set; } = 40;
    public int MinuteSpacing { get; set; } = 2;
    public int KilometerSpacing { get; set; } = 10;
}

public record EventAt(EventType Event, int Minute, int Kilometer)
{

}

public enum TimeAxisDirection
{
    Horisontal,
    Vertical
}

public enum EventType
{
    None,
    MouseEnter,
    MouseExit,
    MouseDown,
    MouseUp,
    MouseMove
}
