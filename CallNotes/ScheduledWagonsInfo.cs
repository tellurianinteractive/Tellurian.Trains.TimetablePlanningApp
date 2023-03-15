using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;
public class ScheduledWagonsInfo {
    public ScheduledWagonsInfo()
    {
        OperationDays = OperationDays.Daily;
    }
    public string? Description { get; set; }
    public required int PositionInTrain { get; init; }
    public required int TurnusNumber { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public OperationDays OperationDays { get; init; }

    public string Markup(OperationDays scheduledDays, bool alwaysShowOperationDays = false) => 
        MarkupExtensions.Div(
            alwaysShowOperationDays ? string.Concat(OperationDays.ShortName.SpanDays(), ToString().SpanValue()) :
            OperationDays.IsAllOtherDays(scheduledDays) ? ToString().SpanValue() :
            OperationDays.IsAnyOtherDays(scheduledDays) ? string.Concat(OperationDays.ShortName.SpanDays(), ToString().SpanValue()) :
            string.Empty
        );

    public override string ToString() => $"{Description} {Resources.Notes.Turnus} {TurnusNumber}".TrimStart();
}
