using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Data;

namespace TimetablePlanning.Models.CallNotes.Extensions;
public static class BlockNoteExtensions
{
    public static IEnumerable<BlockArrivalNote> AsBlockArrivalNotes(this IEnumerable<BlockArrival> blockArrivals) =>
        blockArrivals.GroupBy(b => b.CallId).Select(g => g.AsBlockArrivalNote());

    public static BlockArrivalNote AsBlockArrivalNote(this IEnumerable<BlockArrival> sameCallBlockArrivals) =>
        new()
        {
            ForCallId = sameCallBlockArrivals.First().CallId,
            DutyOperationDays = sameCallBlockArrivals.First().DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = sameCallBlockArrivals.First().TrainOperatingDaysFlags.AsOperationDays(),
            Destinations = sameCallBlockArrivals.Select(d =>
                new BlockInfo
                {
                    PositionInTrain = d.PositionInTrain,
                    OriginName = d.OriginFullName,
                    DestinationName = d.DestinationFullName,
                    DestinationForeColor = d.DestinationForeColor,
                    DestinationBackColor = d.DestinationBackColor,
                    UncoupleHere = d.Uncouple,
                }),
        };
}
