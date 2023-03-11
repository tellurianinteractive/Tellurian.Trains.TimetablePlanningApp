namespace TimetablePlanning.Models.CallNotes.Data;
public abstract class BlockEvent : NoteEvent
{
    public int PositionInTrain { get; init; }
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

public sealed class BlockDisconnectEvent : BlockEvent
{
}

public sealed class BlockConnectEvent : BlockEvent
{
}

