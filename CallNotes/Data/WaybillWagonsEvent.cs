namespace TimetablePlanning.Models.CallNotes.Data;
public abstract class WaybillWagonsEvent : NoteEvent
{
    public int PositionInTrain { get; init; }
    public required byte OperationDaysFlag { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public bool AlsoSwitch { get; init; }
    public bool OriginAndBefore { get; init; }
    public bool DestinationAndBeyond { get; init; }
    public required string OriginFullName { get; init; }
    public required string OriginSignature { get; init; }
    public required string DestinationFullName { get; init; }
    public required string DestinationSignature { get; init; }
    public string? DestinationForeColor { get; init; }
    public string? DestinationBackColor { get; init; }
}

public sealed class WaybillWagonsDisconnectEvent : WaybillWagonsEvent
{
}

public sealed class WaybillWagonsConnectEvent : WaybillWagonsEvent
{
}

