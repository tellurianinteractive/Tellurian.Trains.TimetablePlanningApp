namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class WagonRecord : NoteRecord
{
    public int PositionInTrain { get; init; }
    public required byte OperationDaysFlags { get; init; }
    public required string OriginStationName { get; init; }
    public required string DestinationName { get; init; }

}
