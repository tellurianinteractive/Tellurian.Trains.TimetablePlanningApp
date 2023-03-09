namespace TimetablePlanning.Data;

public abstract class NoteEvent
{
    public required int CallId { get; init; }
    public required byte TrainOperatingDaysFlags { get; init; }
    public required byte DutyOperatingDaysFlags { get; init; }
}
