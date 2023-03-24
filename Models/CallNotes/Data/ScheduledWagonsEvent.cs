namespace TimetablePlanning.Models.CallNotes.Data;
public abstract class ScheduledWagonsEvent : NoteEvent 
{
    public int PositionInTrain { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public required byte OperationDayFlags { get; init; }
    public required int TurnusNumber { get; init; }
    public string? Description { get; init; }
}

public sealed class ScheduledWagonsConnectEvent : ScheduledWagonsEvent
{

}
public sealed class ScheduledWagonsDisconnectEvent : ScheduledWagonsEvent {

}
