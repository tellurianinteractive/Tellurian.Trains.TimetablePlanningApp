namespace TimetablePlanning.Models.Common;
public record TrainPattern(string Name, string Colour)
{
    public required IEnumerable<StationCall> Calls { get; init; }

}
