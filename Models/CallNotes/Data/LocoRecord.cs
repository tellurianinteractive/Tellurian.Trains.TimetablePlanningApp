using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class LocoRecord : NoteRecord
{
    public required string LocoOperatorSignature { get; set; }
    public required string LocoClass { get; set; }
    public string? LocoNumber { get; set; }
    public required string TurnusNumber { get; set; }
    public required byte LocoOperationDaysFlags { get; set; }
    public override string ToString() => $"{GetType().Name} Turnus {TurnusNumber} {LocoOperationDaysFlags.ToOperationDays().ShortName}: {LocoOperatorSignature} {LocoClass} {LocoNumber} ";
}
public sealed class LocoConnectRecord : LocoRecord
{
    public bool CollectFromStagingArea { get; init; }
    public override string ToString() => CollectFromStagingArea ? $"{base.ToString()} {nameof(CollectFromStagingArea)}" : base.ToString();
}

public sealed class LocoDisconnectRecord : LocoRecord
{
    public bool DriveToStagingArea { get; init; }
    public override string ToString() => DriveToStagingArea ? $"{base.ToString()} {nameof(DriveToStagingArea)}" : base.ToString();
}

public sealed class LocoExchangeRecord : NoteRecord
{
    public required byte LocoOperationDaysFlags { get; set; }
    public required byte ReplacingLocoOperationDaysFlags { get; init; }
}

public sealed class LocoTurnOrCirculateRecord : NoteRecord
{
    public required byte LocoOperationDaysFlags { get; init; }
    public bool IsDoubleDirection { get; init; }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }

}
