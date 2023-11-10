namespace TimetablePlanning.Models.Common;

public record Timetable()
{
    public List<Train> Trains { get; } = [];
}
