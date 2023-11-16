using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class TrainMeetNoteMappings
{

    public static IEnumerable<TrainMeetsOrPassesCallNote> ToTrainMeetCallNotes(this IEnumerable<TrainMeetOrPassRecord> records) =>
        records.Where(r => r.TrainDirection != r.OtherTrainDirection)
        .GroupBy(r => r.CallId).Select(e => e.ToTrainMeetsCallNote());
    public static IEnumerable<TrainMeetsOrPassesCallNote> ToTrainPassingCallNotes(this IEnumerable<TrainMeetOrPassRecord> records) =>
        records.GroupBy(r => r.CallId).Select(e => e.ToTrainPassesCallNote());

    private static TrainMeetsCallNote ToTrainMeetsCallNote(this IEnumerable<TrainMeetOrPassRecord> records)
    {
        var common = records.First();
        var result = new TrainMeetsCallNote
        {
            ForCallId = common.CallId,
            DutyOperationDays = common.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = common.TrainOperationDaysFlags.ToOperationDays(),
            TrainNumber = common.TrainNumber,
            TrainCall = new()
            {
                ArrivalTime = common.TrainArrivalTime,
                DepartureTime = common.TrainDepartureTime,
            }
        };
        foreach (var record in records)
        {
            var meetingTrain = new TrainInfo
            {
                Direction = record.OtherTrainDirection,
                Number = record.OtherTrainNumber,
                OperationDays = record.OtherTrainOperationDaysFlags.ToOperationDays(),
                OperatorSignature = record.OtherTrainOperatorSignature,
            };
            var meetingTrainCall = new TrainCallInfo()
            {
                ArrivalTime = record.OtherTrainArrivalTime,
                DepartureTime = record.OtherTrainDepartureTime,
            };
            result.OtherTrains.Add((meetingTrain, meetingTrainCall));
        }
        return result;
    }

    private static TrainPassesCallNote ToTrainPassesCallNote(this IEnumerable<TrainMeetOrPassRecord> records)
    {
        var common = records.First();
        var result = new TrainPassesCallNote
        {
            ForCallId = common.CallId,
            DutyOperationDays = common.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = common.TrainOperationDaysFlags.ToOperationDays(),
            TrainNumber = common.TrainNumber,
            TrainCall = new()
            {
                ArrivalTime = common.TrainArrivalTime,
                DepartureTime = common.TrainDepartureTime,
            }
        };
        foreach (var record in records)
        {
            var meetingTrain = new TrainInfo
            {
                Direction = record.OtherTrainDirection,
                Number = record.OtherTrainNumber,
                OperationDays = record.OtherTrainOperationDaysFlags.ToOperationDays(),
                OperatorSignature = record.OtherTrainOperatorSignature,
            };
            var meetingTrainCall = new TrainCallInfo()
            {
                ArrivalTime = record.OtherTrainArrivalTime,
                DepartureTime = record.OtherTrainDepartureTime,
            };
            result.OtherTrains.Add((meetingTrain, meetingTrainCall));
        }
        return result;
    }
}

