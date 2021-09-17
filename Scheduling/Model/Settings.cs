using System;

namespace Tellurian.Trains.Scheduling.Model
{
    public record Settings
    {
        public TimeSpan StartTime { get; init; } = TimeSpan.FromHours(6);
        public TimeSpan EndTime { get; init; } = TimeSpan.FromHours(20);
        public Offset TimeAxisSpacing { get; init; } = new(30, 30);
        public Offset KilometerAxisSpacing { get; init; } = new(100, 60);
        public int EndMargin { get; init; } = 20;
        public int TrackSpacing { get; init; } = 8;
        public int MinStationSpacing { get; init; } = 40;
        public int MinuteSpacing { get; set; } = 2;
        public int KilometerSpacing { get; set; } = 10;
    }

    public struct Offset
    {
        public Offset(int x, int y) { X = x; Y = y; IsInvalid = false; }
        public int X { get; }
        public int Y { get; }
        public bool IsInvalid { get; private set; }

        public static Offset Invalid => new(0, 0) { IsInvalid = true };
        public static Offset operator +(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X + b.X, a.Y + b.Y);
        public static Offset operator -(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X - b.X, a.Y - b.Y);
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
}
