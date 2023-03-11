using System;

namespace TimetablePlanning.Models.CallNotes.Data;
public class TrainMeetEvent : NoteEvent {
    public required int TrainNumber { get; set; }
    public string? MeetingTrainPrefix { get; init; }
    public required int MeetingTrainNumber { get; init; }
    public string? MeetingTrainOperatorSignature { get; init; }
    public required byte MeetingTrainOperatingDaysFlags { get; init; }
    public required TimeSpan FromTime { get; init; }
    public required TimeSpan ToTime { get; init;}
}
