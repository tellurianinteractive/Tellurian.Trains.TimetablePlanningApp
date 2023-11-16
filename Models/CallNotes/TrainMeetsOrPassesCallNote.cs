using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;
public abstract class TrainMeetsOrPassesCallNote : TrainCallNote
{
    public TrainMeetsOrPassesCallNote()
    {
        IsForArrival = true;
        IsToLocoDriver = true;
        IsToDispatcher = true;
    }

    public required string TrainNumber { get; init; }
    public string? OperatorSignature { get; init; }
    public required TrainCallInfo TrainCall { get; init; }
    public List<(TrainInfo train, TrainCallInfo call)> OtherTrains { get; init; } = [];

    public override MarkupString Markup() => new(MarkupText.Div());
    private string MarkupText => LocalizedText.SpanText() + string.Join(", ", OtherTrains
        .Where(mt => TrainCall.IsOverlapping(mt.call))
        .Select(mt => string.Concat(TrainDays(mt.train).SpanDays(), TrainIdentity(mt.train).SpanValue(), TimeWhenBothTrainsAreAtStation(mt.train, mt.call).SpanValue())));

    private OperationDays NoteDaysSpecialized(TrainInfo train) => NoteDays & train.OperationDays;


    private string TrainIdentity(TrainInfo otherTrain) =>
        otherTrain.OperatorSignature.HasNoValue() || otherTrain.OperatorSignature?.Equals(OperatorSignature) == true ? otherTrain.Number :
        $"{otherTrain.OperatorSignature} {otherTrain.Number}";

    private string TrainDays(TrainInfo train)
    {
        var days = NoteDaysSpecialized(train);
        if (days.IsNoDays || OtherTrains.All(ot => ot.train.OperationDays.IsAllDaysOf(OperationDays))) return string.Empty;
        return days.ShortName;
    }
    private string TimeWhenBothTrainsAreAtStation(TrainInfo otherTrain, TrainCallInfo otherCall)
    {
        var days = NoteDaysSpecialized(otherTrain);
        if (days.IsNoDays) return string.Empty;
        return Time(otherCall);
    }
    private string Time(TrainCallInfo meetingTrainCall) => $"{TrainCall.ArrivalTime.Max(meetingTrainCall.ArrivalTime):hh\\:mm}-{TrainCall.DepartureTime.Min(meetingTrainCall.DepartureTime):hh\\:mm}";

    protected abstract string LocalizedText { get; }

    protected override OperationDays NoteDays => TrainOperationDays;
}

public sealed class TrainMeetsCallNote : TrainMeetsOrPassesCallNote
{
    protected override string LocalizedText => Resources.Notes.Meets;
}

public sealed class TrainPassesCallNote : TrainMeetsOrPassesCallNote
{
    protected override string LocalizedText => Resources.Notes.Passes;
}

