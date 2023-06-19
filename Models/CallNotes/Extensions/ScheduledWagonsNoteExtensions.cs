using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;
internal static class ScheduledWagonsNoteExtensions
{
    public static IEnumerable<ScheduledWagonsConnectNote> AsScheduledWagonsConnectNotes(this IEnumerable<ScheduledWagonsConnectEvent> events) =>
        events.GroupBy(e => e.CallId).Select(g => g.AsScheduledWagonsConnectNote());

    public static ScheduledWagonsConnectNote AsScheduledWagonsConnectNote(this IEnumerable<ScheduledWagonsConnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Wagons = events.Select(e => e.AsScheduledWagonsInfo()),

        };

    public static IEnumerable<ScheduledWagonsDisconnectNote> AsScheduledWagonsDisconnectNotes(this IEnumerable<ScheduledWagonsDisconnectEvent> events) =>
        events.GroupBy(e => e.CallId).Select(g => g.AsScheduledWagonsDisconnectNote());

    public static ScheduledWagonsDisconnectNote AsScheduledWagonsDisconnectNote(this IEnumerable<ScheduledWagonsDisconnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Wagons = events.Select(e => e.AsScheduledWagonsInfo()),
        };

    private static ScheduledWagonsInfo AsScheduledWagonsInfo(this ScheduledWagonsEvent e) =>
        new()
        {
            PositionInTrain = e.PositionInTrain,
            Description = e.Description,
            OperationDays = e.OperationDayFlags.AsOperationDays(),
            TurnusNumber = e.TurnusNumber,
            MaxNumberOfWagons = e.MaxNumberOfWagons,
        };
}


