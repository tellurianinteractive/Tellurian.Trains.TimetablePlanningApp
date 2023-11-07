using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;
internal static class ScheduledWagonsNoteMappings
{
    public static IEnumerable<ScheduledWagonsConnectNote> ToScheduledWagonsConnectNotes(this IEnumerable<ScheduledWagonsConnectRecord> records) =>
        records.GroupBy(e => e.CallId).Select(g => g.ToScheduledWagonsConnectNote());

    public static ScheduledWagonsConnectNote ToScheduledWagonsConnectNote(this IEnumerable<ScheduledWagonsConnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperationDaysFlags.ToOperationDays(),
            Wagons = records.Select(e => e.ToScheduledWagonsInfo()),

        };

    public static IEnumerable<ScheduledWagonsDisconnectNote> ToScheduledWagonsDisconnectNotes(this IEnumerable<ScheduledWagonsDisconnectRecord> records) =>
        records.GroupBy(e => e.CallId).Select(g => g.ToScheduledWagonsDisconnectNote());

    public static ScheduledWagonsDisconnectNote ToScheduledWagonsDisconnectNote(this IEnumerable<ScheduledWagonsDisconnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperationDaysFlags.ToOperationDays(),
            Wagons = records.Select(e => e.ToScheduledWagonsInfo()),
        };

    private static ScheduledWagonsInfo ToScheduledWagonsInfo(this ScheduledWagonsRecord record) =>
        new()
        {
            PositionInTrain = record.PositionInTrain,
            Description = record.Description,
            OperationDays = record.OperationDaysFlags.ToOperationDays(),
            TurnusNumber = record.TurnusNumber,
            MaxNumberOfWagons = record.MaxNumberOfWagons,
        };
}


