namespace TimetablePlanning.Data;

public abstract class LocoAction : NoteBase
{
    public required string LocoOperatorSignature { get; init; }
    public required string LocoClass { get; init; }
    public string? LocoNumber { get; init; }
    public required string TurnNumber { get; init; }
    public required byte LocoOperatingDaysFlags { get; init; }
    public bool IsDoubleDirectionTrain { get; init; }
}
public class LocoConnect : LocoAction
{
    public bool CollectFromStagingArea { get; init; }
}

public class LocoDisconnect : LocoAction
{
    public bool DriveToStagingArea { get; init; }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
}
