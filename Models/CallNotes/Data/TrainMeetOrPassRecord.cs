namespace TimetablePlanning.Models.CallNotes.Data;

public class TrainMeetOrPassRecord : TrainRecord
{
    public required string OtherTrainNumber { get; init; }
    public string? OtherTrainOperatorSignature { get; init; }
    public required byte OtherTrainOperationDaysFlags { get; init; }
    public required TimeSpan OtherTrainArrivalTime { get; init; }
    public required TimeSpan OtherTrainDepartureTime { get; init; }
    public required int OtherTrainDirection { get; init; }
}

public sealed class TrainMeetsRecord : TrainMeetOrPassRecord { }

public sealed class TrainPassesRecord : TrainMeetOrPassRecord { }