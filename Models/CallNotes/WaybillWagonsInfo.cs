using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public sealed class WaybillWagonsInfo {
    public required int PositionInTrain { get; init; }
    public required OperationDays OperationDays { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public required DestinationInfo Destination { get; init; }
    public required OriginInfo Origin { get; init; }
    public string Markup(OperationDays scheduledDays, bool alwaysShowOperationDays = false) =>
        MarkupExtensions.Div(
            alwaysShowOperationDays ? string.Concat(OperationDays.ShortName.SpanDays(), ToString().SpanValue()) :
            OperationDays.IsAllOtherDays(scheduledDays) ? ToString().SpanValue() :
            OperationDays.IsAnyOtherDays(scheduledDays) ? string.Concat(OperationDays.ShortName.SpanDays(), ToString().SpanValue()) :
            string.Empty
        );
    public override string ToString() => Destination.SpanDestination(MaxNumberOfWagons);

}