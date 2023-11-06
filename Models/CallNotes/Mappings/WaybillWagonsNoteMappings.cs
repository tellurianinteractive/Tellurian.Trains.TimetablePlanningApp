using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class WaybillWagonsNoteMappings
{

    //private const string DefaultDestinationBackColor = "#C0C0C0";

    public static IEnumerable<WagonGroupConnectNote> ToWagonGroupsConnectNotes(this IEnumerable<WagonGroupConnectRecord> records) =>
        records.GroupBy(e => e.CallId).Select(g => g.ToWagonGroupConnectNote());


    public static IEnumerable<WagonGroupDisconnectNote> ToWagonGroupsDisconnectNotes(this IEnumerable<WagonGroupDisconnectRecord> records) =>
        records.GroupBy(b => b.CallId).Select(g => g.ToWagonGroupDisconnectNote());

    private static WagonGroupConnectNote ToWagonGroupConnectNote(this IEnumerable<WagonGroupConnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperatingDaysFlags.ToOperationDays(),
            GroupsDestinations = records.Select(r => r.ToGroupDestination()),
        };

    private static WagonGroupDisconnectNote ToWagonGroupDisconnectNote(this IEnumerable<WagonGroupDisconnectRecord> records) =>
        new()
        {
            ForCallId = records.First().CallId,
            DutyOperationDays = records.First().DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = records.First().TrainOperatingDaysFlags.ToOperationDays(),
            GroupsDestinations = records.Select(r => r.ToGroupDestination()),

        };



}
