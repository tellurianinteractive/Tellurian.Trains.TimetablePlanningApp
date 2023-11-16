namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class TrainRecord : NoteRecord
{
    public required string TrainOperatorSignature { get; init; }
    public required string TrainNumber { get; init; }
    public required TimeSpan TrainArrivalTime { get; init; }
    public required TimeSpan TrainDepartureTime { get; init;}
    public required int TrainDirection { get; init; }

}
