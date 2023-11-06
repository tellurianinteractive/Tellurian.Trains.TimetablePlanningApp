using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;
internal static class ScheduledWagonsNoteMappings
{
    public static IEnumerable<ScheduledWagonsConnectNote> ToScheduledWagonsConnectNotes(this IEnumerable<ScheduledWagonsConnectRecord> events) =>
        events.GroupBy(e => e.CallId).Select(g => g.ToScheduledWagonsConnectNote());

    public static ScheduledWagonsConnectNote ToScheduledWagonsConnectNote(this IEnumerable<ScheduledWagonsConnectRecord> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.ToOperationDays(),
            Wagons = events.Select(e => e.ToScheduledWagonsInfo()),

        };

    public static IEnumerable<ScheduledWagonsDisconnectNote> ToScheduledWagonsDisconnectNotes(this IEnumerable<ScheduledWagonsDisconnectRecord> events) =>
        events.GroupBy(e => e.CallId).Select(g => g.ToScheduledWagonsDisconnectNote());

    public static ScheduledWagonsDisconnectNote ToScheduledWagonsDisconnectNote(this IEnumerable<ScheduledWagonsDisconnectRecord> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.ToOperationDays(),
            Wagons = events.Select(e => e.ToScheduledWagonsInfo()),
        };

    private static ScheduledWagonsInfo ToScheduledWagonsInfo(this ScheduledWagonsRecord e) =>
        new()
        {
            PositionInTrain = e.PositionInTrain,
            Description = e.Description,
            OperationDays = e.OperationDayFlags.ToOperationDays(),
            TurnusNumber = e.TurnusNumber,
            MaxNumberOfWagons = e.MaxNumberOfWagons,
        };
}


