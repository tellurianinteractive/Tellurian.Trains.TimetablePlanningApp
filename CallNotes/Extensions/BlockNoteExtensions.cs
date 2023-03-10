using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;

namespace TimetablePlanning.Models.CallNotes.Extensions;
public static class BlockNoteExtensions {
    public static IEnumerable<BlockDisconnectNote> AsBlockDisconnectNotes(this IEnumerable<BlockDisconnectEvent> blockArrivals) =>
        blockArrivals.GroupBy(b => b.CallId).Select(g => g.AsBlockDisconnectNote());

    public static BlockDisconnectNote AsBlockDisconnectNote(this IEnumerable<BlockDisconnectEvent> sameCallBlockArrivals) =>
        new()
        {
            ForCallId = sameCallBlockArrivals.First().CallId,
            DutyOperationDays = sameCallBlockArrivals.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = sameCallBlockArrivals.First().TrainOperatingDaysFlags.AsOperationDays(),
            Destinations = sameCallBlockArrivals.Select(d =>
                new BlockInfo
                {
                    PositionInTrain = d.PositionInTrain,
                    Origin = new OriginInfo()
                    {
                        FullName = d.OriginFullName
                    },
                    Destination = new DestinationInfo()
                    {
                        FullName = d.DestinationFullName,
                        ForeColor = d.DestinationForeColor,
                        BackColor = d.DestinationBackColor
                    },
                    UncoupleHere = d.Uncouple,
                }),
        };
}
