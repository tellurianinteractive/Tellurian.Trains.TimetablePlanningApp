namespace TimetablePlanning.Models.CallNotes.Data;

    public class TrainMeetRecord : TrainRecord {
    public required string MeetingTrainNumber { get; init; }
    public string? MeetingTrainOperatorSignature { get; init; }
    public required byte MeetingTrainOperationDaysFlags { get; init; }
    public required TimeSpan MeetingTrainArrivalTime { get; init; }
    public required TimeSpan MeetingTrainDepartureTime { get; init; }
    public bool IsPassing { get; init; }

}
