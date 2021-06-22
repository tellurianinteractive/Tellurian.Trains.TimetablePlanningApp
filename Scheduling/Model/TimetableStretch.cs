using System;
using System.Collections.Generic;
using System.Linq;
using Tellurian.Trains.Scheduling.Extensions;

namespace Tellurian.Trains.Scheduling.Model
{
    public record Schedule()
    {
        public List<Train> Trains { get; } = new List<Train>();

    }

    public record Train(string Number)
    {

    }
    public record StationCall(StationTrack Track, TimeSpan Time)
    {
        public bool IsHidden { get; init; }
    }
    public record TrainLink(StationCall Arrival, StationCall Departure)
    {
        public bool IsStop { get; init; } = true;
    }
    public record TimetableStretch(string Description)
    {
        public TrackStretch[] TrackStreches { get; init; } = Array.Empty<TrackStretch>();
        public Station[] Stations => new[] { TrackStreches[0].From }.Concat(TrackStreches.Select(ts => ts.To)).ToArray();
        public Settings Settings { get; init; } = new Settings();
        public TimeAxisDirection TimeAxisDirection { get; init; }

        public int Height =>
            TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => this.MaxTrackOffset().Y + Settings.EndMargin,
                TimeAxisDirection.Vertical => MaxTimeOffset.Y + Settings.EndMargin,
                _ => 0
            };

        public int Width =>
            TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => MaxTimeOffset.X + Settings.EndMargin,
                TimeAxisDirection.Vertical => this.MaxTrackOffset().X + Settings.EndMargin,
                _ => 0
            };

        public bool HasHorizontalTimeAxis => TimeAxisDirection == TimeAxisDirection.Horisontal;
        public bool HasVerticalTimeAxis => TimeAxisDirection == TimeAxisDirection.Vertical;
        public TimeSpan StartTime => Settings.StartTime;
        public TimeSpan EndTime => Settings.EndTime;
        public Offset MaxTrackOffset => this.MaxTrackOffset();
        public Offset MaxTimeOffset => this.TimeOffset(EndTime);

    }

    public record TrackStretch(Station From, Station To)
    {
        public int Km => Math.Abs(From.Km - To.Km);
    }

    public record Station(string Name, string Signature)
    {
        public int Km { get; init; }
        public StationTrack[] Tracks { get; init; } = Array.Empty<StationTrack>();
        public bool HasPassengerExchange => Tracks.Any(t => t.HasPlatform);
    }

    public record StationTrack(string Number) { public bool HasPlatform { get; init; } = true; }
}
