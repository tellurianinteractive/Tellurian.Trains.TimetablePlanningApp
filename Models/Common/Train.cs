namespace TimetablePlanning.Models.Common;

public record Timetable()
{
    public List<Train> Trains { get; } = new List<Train>();
}

public record Train(string Number)
{

}

public record TrainLink(StationCall Arrival, StationCall Departure)
{
    public bool IsStop { get; init; } = true;
}

public record StationCall(StationTrack Track, TimeSpan Time)
{
    public bool IsHidden { get; init; }
}
