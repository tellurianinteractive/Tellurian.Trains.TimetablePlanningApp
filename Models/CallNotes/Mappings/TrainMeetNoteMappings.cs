using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class TrainMeetNoteMappings
{

    public static IEnumerable<TrainCallMeetNote> ToTrainMeetNotes(this IEnumerable<TrainMeetRecord> events) =>
        events.Select(e => e.ToTrainMeetNote());

    public static TrainCallMeetNote ToTrainMeetNote(this TrainMeetRecord e) =>
        new()
        {
            ForCallId = e.CallId,
            TrainNumber = e.TrainNumber,
            TrainCall = new()
            {
                ArrivalTime = e.TrainArrivalTime,
                DepartureTime = e.TrainDepartureTime
            },
            DutyOperationDays = e.DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = e.TrainOperatingDaysFlags.ToOperationDays(),
            MeetingTrain = new()
            {
                Number = e.MeetingTrainNumber,
                Prefix = e.MeetingTrainPrefix,
                OperatorSignature = e.MeetingTrainOperatorSignature,
                OperationDays = e.MeetingTrainOperatingDaysFlags.ToOperationDays(),
            },
            MeetingTrainCall = new()
            {
                ArrivalTime = e.MeetingTrainArrivalTime,
                DepartureTime = e.MeetingDepartureTime
            }
        };
}
