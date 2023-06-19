namespace TimetablePlanning.Models.Common;
public record Station(string Name, string Signature)
{
    public int Km { get; init; }
    public StationTrack[] Tracks { get; init; } = Array.Empty<StationTrack>();
    public bool HasPassengerExchange => Tracks.Any(t => t.HasPlatform);
}
public record StationTrack(string Number) { 
    public bool HasPlatform { get; init; } = true; 
    public bool IsScheduled { get; init; } = true; 
}
