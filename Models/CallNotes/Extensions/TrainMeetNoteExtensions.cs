using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;

internal static class TrainMeetNoteExtensions
{

    public static IEnumerable<TrainMeetNote> AsTrainMeetNotes(this IEnumerable<TrainMeetEvent> events) =>
        events.Select(e => e.AsTrainMeetNote());

    public static TrainMeetNote AsTrainMeetNote(this TrainMeetEvent e) =>
        new()
        {
            ForCallId = e.CallId,
            TrainNumber = e.TrainNumber,
            TrainCall = new ()
            {
                ArrivalTime = e.TrainArrivalTime,
                DepartureTime = e.TrainDepartureTime
            },
            DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
            MeetingTrain = new()
            {
                Number = e.MeetingTrainNumber,
                Prefix = e.MeetingTrainPrefix,
                OperatorSignature = e.MeetingTrainOperatorSignature,
                OperationDays = e.MeetingTrainOperatingDaysFlags.AsOperationDays(),
            },
            MeetingTrainCall = new ()
            {
                ArrivalTime = e.MeetingTrainArrivalTime,
                DepartureTime = e.MeetingDepartureTime
            }
        };
}
