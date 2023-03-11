using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;
public static class BlockNoteExtensions {

    private const string DefaultDestinationForeColor = "#000000";
    private const string DefaultDestinationBackColor = "#C0C0C0";

    public static IEnumerable<WaybillWagonsConnectNote> AsBlockConnectNotes(this IEnumerable<BlockConnectEvent> events) =>
        events.GroupBy(b => b.CallId).Select(g => g.AsBlockConnectNote());


    public static WaybillWagonsConnectNote AsBlockConnectNote(this IEnumerable<BlockConnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Blocks = events.Select(d => d.AsBlockInfo()),
        };


    public static IEnumerable<WaybillWagonsDisconnectNote> AsBlockDisconnectNotes(this IEnumerable<BlockDisconnectEvent> events) =>
        events.GroupBy(b => b.CallId).Select(g => g.AsBlockDisconnectNote());

    public static WaybillWagonsDisconnectNote AsBlockDisconnectNote(this IEnumerable<BlockDisconnectEvent> events) =>
        new()
        {
            ForCallId = events.First().CallId,
            DutyOperationDays = events.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = events.First().TrainOperatingDaysFlags.AsOperationDays(),
            Blocks = events.Select(d => d.AsBlockInfo()),
        };

    private static WaybillWagonGroup AsBlockInfo(this BlockEvent e) =>
        new()
        {
            PositionInTrain = e.PositionInTrain,
            MaxNumberOfWagons = e.MaxNumberOfWagons,
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
