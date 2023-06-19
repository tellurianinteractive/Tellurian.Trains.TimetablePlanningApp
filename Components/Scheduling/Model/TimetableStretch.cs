using TimetablePlanning.Components.Scheduling.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling;

public record TimetableStretch(string Description)
{
    public TrackStretch[] TrackStreches { get; init; } = Array.Empty<TrackStretch>();
    public Station[] Stations => new[] { TrackStreches[0].From }.Concat(TrackStreches.Select(ts => ts.To)).ToArray();
    public Settings Settings { get; init; } = new Settings();

      public TimeSpan StartTime => Settings.StartTime;
    public TimeSpan EndTime => Settings.EndTime;
}


