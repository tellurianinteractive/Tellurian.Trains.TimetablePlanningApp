using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class TrainMeetNoteMappings
{

    public static IEnumerable<TrainCallMeetNote> ToTrainMeetNotes(this IEnumerable<TrainMeetRecord> records) =>
        records.Select(e => e.ToTrainMeetNote());

    public static TrainCallMeetNote ToTrainMeetNote(this TrainMeetRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            TrainNumber = record.TrainNumber,
            TrainCall = new()
            {
                ArrivalTime = record.TrainArrivalTime,
                DepartureTime = record.TrainDepartureTime
            },
            DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
            MeetingTrain = new()
            {
                Number = record.MeetingTrainNumber,
                OperatorSignature = record.MeetingTrainOperatorSignature,
                OperationDays = record.MeetingTrainOperationDaysFlags.ToOperationDays(),
            },
            MeetingTrainCall = new()
            {
                ArrivalTime = record.MeetingTrainArrivalTime,
                DepartureTime = record.MeetingTrainDepartureTime
            },
            IsPassing = record.IsPassing,
        };
}
