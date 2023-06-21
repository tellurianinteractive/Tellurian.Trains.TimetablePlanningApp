using System.ComponentModel.DataAnnotations;

namespace TimetablePlanning.Models.Common;

public record StationTrack(string Number) { 

    public Station? Station { get; set; }
    public int PlatformLength { get; init; }
    public bool HasPlatform  => PlatformLength > 0; 
    public bool IsScheduled { get; init; } = true;
    public override string ToString() => Number;
}
