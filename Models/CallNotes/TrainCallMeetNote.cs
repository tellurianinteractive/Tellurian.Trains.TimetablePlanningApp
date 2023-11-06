using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;
public class TrainCallMeetNote : TrainCallNote
{
    public TrainCallMeetNote()
    {
        IsForArrival = true;
        IsToLocoDriver = true;
        IsToDispatcher = true;
    }

    public required int TrainNumber { get; init; }
    public required TrainCallInfo TrainCall { get; init; }
    public required TrainInfo MeetingTrain { get; init; }
    public required TrainCallInfo MeetingTrainCall { get; init; }

    public override MarkupString Markup() => new(MarkupText);
    private string MarkupText => TrainCall.IsOverlapping(MeetingTrainCall) ?
            NoteDays.IsAnyOtherDays(ServiceOperationDays) ? string.Concat(Days,LocalizedText, MeetingTrain.Markup, Time) :
            string.Empty :
        string.Empty;

    private string LocalizedText => TrainNumber.BothIsOddOrEven(MeetingTrain.Number) ? Resources.Notes.Passes.SpanText() : Resources.Notes.Meets.SpanText();
    private string Time => $"{TrainCall.ArrivalTime.Max(MeetingTrainCall.ArrivalTime):hh\\:mm}-{TrainCall.DepartureTime.Min(MeetingTrainCall.DepartureTime):hh\\:mm}".SpanValue();
    private string Days => NoteDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.SpanDays();
    private OperationDays NoteDays => MeetingTrain.OperationDays & ServiceOperationDays;
}
