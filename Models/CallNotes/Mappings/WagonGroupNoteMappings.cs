using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class WagonGroupNoteMappings
{    public static IEnumerable<WagonGroupConnectNote> ToWagonGroupsConnectNotes(this IEnumerable<WagonGroupConnectRecord> records) =>
        records.GroupBy(e => e.CallId).Select(g => g.ToWagonGroupConnectNote());

    public static IEnumerable<WagonGroupDisconnectNote> ToWagonGroupsDisconnectNotes(this IEnumerable<WagonGroupDisconnectRecord> records) =>
        records.GroupBy(b => b.CallId).Select(g => g.ToWagonGroupDisconnectNote());

    public static IEnumerable<WagonGroupFromCustomersCallNote> ToWagonGroupFromCustomersCallNotes(this IEnumerable<WagonGroupFromCustomersRecord> records) =>
        records.Select(r => r.ToWagonGroupFromCustomersCallNote());

    public static IEnumerable<WagonGroupToCustomersCallNote> ToWagonGroupToCustomersCallNote(this IEnumerable<WagonGroupToCustomersRecord> records) =>
        records.Select(r => r.ToWagonGroupToCustomersCallNote());

    private static WagonGroupConnectNote ToWagonGroupConnectNote(this IEnumerable<WagonGroupConnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperationDaysFlags.ToOperationDays(),
            GroupDestinations = records
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
            GroupDestinations = records
                .OrderBy(r=> r.PositionInTrain)
                .ThenBy(r => r.DisplayOrder)
                .Select(r => r.ToGroupDestination()),
        };

    private static WagonGroupFromCustomersCallNote ToWagonGroupFromCustomersCallNote(this WagonGroupFromCustomersRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
        }; 
    private static WagonGroupToCustomersCallNote ToWagonGroupToCustomersCallNote(this WagonGroupToCustomersRecord record) =>
        new() { 
             ForCallId = record.CallId,
             DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
             TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
        };
}
