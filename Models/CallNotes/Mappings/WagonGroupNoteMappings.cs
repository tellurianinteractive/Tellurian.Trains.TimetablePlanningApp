using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class WagonGroupNoteMappings
{    public static IEnumerable<WagonGroupConnectNote> ToWagonGroupsConnectNotes(this IEnumerable<WagonGroupConnectRecord> records) =>
        records.GroupBy(e => e.CallId).Select(g => g.ToWagonGroupConnectNote());

    public static IEnumerable<WagonGroupDisconnectNote> ToWagonGroupsDisconnectNotes(this IEnumerable<WagonGroupDisconnectRecord> records) =>
        records.GroupBy(b => b.CallId).Select(g => g.ToWagonGroupDisconnectNote());

    private static WagonGroupConnectNote ToWagonGroupConnectNote(this IEnumerable<WagonGroupConnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperationDaysFlags.ToOperationDays(),
            GroupsDestinations = records
                .OrderBy(r => r.PositionInTrain)
                .ThenBy(r => r.DisplayOrder)
                .Select(r => r.ToGroupDestination()),
        };

    private static WagonGroupDisconnectNote ToWagonGroupDisconnectNote(this IEnumerable<WagonGroupDisconnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperationDaysFlags.ToOperationDays(),
            GroupsDestinations = records
                .OrderBy(r=> r.PositionInTrain)
                .ThenBy(r => r.DisplayOrder)
                .Select(r => r.ToGroupDestination()),
        };
}
