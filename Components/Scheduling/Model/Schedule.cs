using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling;

public class Schedule
{
    internal Schedule(string description, IEnumerable<Station> stations, IEnumerable<Train> trains, GraphSettings? graphSettings = null)
    {
        Description = description;
        Stations = stations.ToArray();
        Streches = CreateStretchesFrom(stations).ToArray();
        GraphSettings = graphSettings ?? GraphSettings.Default;
        _Trains.AddRange(trains);
        StartTime = GetStartTime;
        EndTime = GetEndTime;
    }
    public GraphSettings GraphSettings { get; }
    public string Description { get; }
    public Station[] Stations { get; }
    public TrackStretch[] Streches { get; }
    public Train[] Trains => _Trains.ToArray();

    public TimeSpan StartTime { get; }

    private TimeSpan GetStartTime
    {
        get
        {
            if (Trains.Any())
            {
                var time = Trains.SelectMany(t => t.Calls).Select(c => c.Arrival).Min(d => d.Time);
                return new TimeSpan(time.Days, time.Hours , 0, 0);
            }

            else return GraphSettings.StartTime;
        }
    }

    public TimeSpan EndTime { get; }
    private TimeSpan GetEndTime
    {
        get
        {
            if (Trains.Any()) {
                var time = Trains.SelectMany(t => t.Calls).Select(c => c.Departure).Max(d => d.Time);
                return new TimeSpan(time.Days, time.Hours+1 , 0, 0);
            }
            else return GraphSettings.EndTime;
        }
    }

    private readonly List<Train> _Trains = new();

    private static IEnumerable<TrackStretch> CreateStretchesFrom(IEnumerable<Station> stations)
    {
        var result = new List<TrackStretch>(stations.Count());
        var ss = stations.ToArray();
        for (var i = 1; i < ss.Length; i++)
        {
            result.Add(new TrackStretch(ss[i - 1], ss[i]));
        }
        return result;
    }
}




