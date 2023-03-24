using System;

namespace TimetablePlanning.Components.Scheduling;

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
