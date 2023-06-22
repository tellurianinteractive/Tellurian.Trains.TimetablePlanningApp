namespace TimetablePlanning.Models.Common;

public record StationCall(StationTrack Track, CallAction Arrival, CallAction Departure)
{
    public bool IsStop { get; init; } = true;
    public override string ToString() => $"{Track} {Arrival}-{Departure}";
    public Train? Train { get; init; }
}

public record CallAction(TimeSpan Time)
{
    public bool IsHidden { get; init; }
    public override string ToString() => $"{Time}:HH:mm";
}

public record StretchUse(Train Train, int FromCallIndex) { 
    public StationCall From => Train.Calls[FromCallIndex];
    public StationCall To => Train.Calls[FromCallIndex + 1];
}
