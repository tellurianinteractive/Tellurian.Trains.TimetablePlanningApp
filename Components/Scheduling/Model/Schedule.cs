using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling;

public class Schedule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="description">Descriptive name of schedule.</param>
    /// <param name="stations">Stations in order on the stretch.</param>
    /// <param name="trains">Trains that runs on this stretch, also partially.</param>
    /// <param name="graphSettings">Display parameters.</param>
    internal Schedule(string description, IEnumerable<Station> stations, IEnumerable<Train> trains, GraphSettings? graphSettings = null)
    {
        Description = description;
        Stations = stations.ToArray();
        Streches = [.. CreateStretchesFrom(stations)];
        GraphSettings = graphSettings ?? GraphSettings.Default;
        _Trains.AddRange(trains);
        StartTime = GetStartTime();
        EndTime = GetEndTime();
    }
    public GraphSettings GraphSettings { get; }
    public TimeAxisDirection AxisDirection => GraphSettings.AxisDirection;
    public string Description { get; }
    public Station[] Stations { get; }
    public TrackStretch[] Streches { get; }
    public Train[] Trains => [.. _Trains];
    private readonly List<Train> _Trains = [];

    public void Add(Train train)
    {
        _Trains.Add(train);
        StartTime = GetStartTime();
        EndTime = GetEndTime();
    }

    public TimeSpan StartTime { get; private set; }

    private TimeSpan GetStartTime()
    {
        if (Trains.Length > 0)
        {
            var time = Trains.SelectMany(t => t.Calls).Select(c => c.Arrival).Min(d => d.Time);
            return new TimeSpan(time.Days, time.Hours, 0, 0);
        }

        else
            return GraphSettings.DefaultStartTime;
    }

    public TimeSpan EndTime { get; private set; }
    private TimeSpan GetEndTime()
    {
        if (Trains.Length > 0)
        {
            var time = Trains.SelectMany(t => t.Calls).Select(c => c.Departure).Max(d => d.Time);
            return new TimeSpan(time.Days, time.Hours + 1, 0, 0);
        }
        else 
            return GraphSettings.DefaultEndTime;
    }


    private static List<TrackStretch> CreateStretchesFrom(IEnumerable<Station> stations)
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




