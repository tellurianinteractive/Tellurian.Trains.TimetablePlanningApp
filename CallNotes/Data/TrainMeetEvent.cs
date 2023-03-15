using System;

namespace TimetablePlanning.Models.CallNotes.Data;
public class TrainMeetEvent : NoteEvent {
    public required int TrainNumber { get; set; }
    public required TimeSpan TrainArrivalTime { get; init; }
    public required TimeSpan TrainDepartureTime { get; init;}
    public string? MeetingTrainPrefix { get; init; }
    public required int MeetingTrainNumber { get; init; }
    public string? MeetingTrainOperatorSignature { get; init; }
    public required byte MeetingTrainOperatingDaysFlags { get; init; }
    public required TimeSpan MeetingTrainArrivalTime { get; init; }
    public required TimeSpan MeetingDepartureTime { get; init; }

}
