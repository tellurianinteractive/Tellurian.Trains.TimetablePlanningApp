namespace TimetablePlanning.Data;
public abstract class BlockAction : NoteBase
{
    public int PositionInTrain { get; init; }
    public required string OriginFullName { get; init; }
    public required string OriginSignature { get; init; }
    public bool OriginAndBefore { get; init; }
    public required string DestinationFullName { get; init; }
    public required string DestinationSignature { get; init; }
    public bool DestinationAndBeyond { get; init; }
    public string DestinationForeColor { get; init; } = "#000000";
    public string DestinationBackColor { get; init; } = "#C0C0C0";
}

public sealed class BlockArrival : BlockAction
{
    public bool Uncouple { get; init; }
    public bool AlsoSwitch { get; init; }
}

