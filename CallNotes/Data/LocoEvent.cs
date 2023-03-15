namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class LocoEvent : NoteEvent
{
    public required string LocoOperatorSignature { get; init; }
    public required string LocoClass { get; init; }
    public string? LocoNumber { get; init; }
    public required int TurnusNumber { get; init; }
    public required byte LocoOperatingDaysFlags { get; init; }
    public bool IsDoubleDirectionTrain { get; init; }
}
public sealed class LocoConnectEvent : LocoEvent
{
    public bool CollectFromStagingArea { get; init; }
}

public sealed class LocoDisconnectEvent : LocoEvent
{
    public bool DriveToStagingArea { get; init; }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
}

public sealed class LocoExchangeEvent : LocoEvent
{
    public required byte ReplacingLocoOperatingDaysFlags { get; init; }
}
