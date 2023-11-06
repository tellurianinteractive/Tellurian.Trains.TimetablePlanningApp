namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class LocoRecord : NoteRecord
{
    public required string LocoOperatorSignature { get; init; }
    public required string LocoClass { get; init; }
    public string? LocoNumber { get; init; }
    public required int TurnusNumber { get; init; }
    public required byte LocoOperatingDaysFlags { get; init; }
    public bool IsDoubleDirectionTrain { get; init; }
}
public sealed class LocoConnectRecord : LocoRecord
{
    public bool CollectFromStagingArea { get; init; }
}

public sealed class LocoDisconnectRecord
    : LocoRecord
{
    public bool DriveToStagingArea { get; init; }
}

public sealed class LocoExchangeRecord : LocoRecord
{
    public required byte ReplacingLocoOperatingDaysFlags { get; init; }
}

public sealed class LocoTurnOrCirculateRecord : NoteRecord
{
    public required byte LocoOperatingDaysFlags { get; init; }
    public bool IsDoubleDirection { get; init; }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }

}
