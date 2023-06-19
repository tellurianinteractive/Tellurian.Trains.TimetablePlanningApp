using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;
internal static class WaybillWagonsNoteExtensions {

    private const string DefaultDestinationForeColor = "#000000";
    private const string DefaultDestinationBackColor = "#C0C0C0";

    public static IEnumerable<WaybillWagonsConnectNote> AsWaybillWagonsConnectNotes(this IEnumerable<WaybillWagonsConnectEvent> events) =>
        events.GroupBy(b => b.CallId).Select(g => g.AsWaybillWagonsConnectNote());


    public static WaybillWagonsConnectNote AsWaybillWagonsConnectNote(this IEnumerable<WaybillWagonsConnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Wagons = events.Select(d => d.AsWaybillWagonInfo()),
        };


    public static IEnumerable<WaybillWagonsDisconnectNote> AsWaybillWagonsDisconnectNotes(this IEnumerable<WaybillWagonsDisconnectEvent> events) =>
        events.GroupBy(b => b.CallId).Select(g => g.AsBlockDisconnectNote());

    public static WaybillWagonsDisconnectNote AsBlockDisconnectNote(this IEnumerable<WaybillWagonsDisconnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Wagons = events.Select(d => d.AsWaybillWagonInfo()),
        };

    private static WaybillWagonsInfo AsWaybillWagonInfo(this WaybillWagonsEvent e) =>
        new()
        {
            PositionInTrain = e.PositionInTrain,
            MaxNumberOfWagons = e.MaxNumberOfWagons,
            OperationDays = e.OperationDaysFlag.AsOperationDays(),
            Origin = new OriginInfo()
            {
                FullName = e.OriginFullName
            },
            Destination = new DestinationInfo()
            {
                FullName = e.DestinationFullName,
                ForeColor = e.DestinationForeColor ?? DefaultDestinationForeColor,
                BackColor = e.DestinationBackColor ?? DefaultDestinationBackColor,
            },
        };
}
