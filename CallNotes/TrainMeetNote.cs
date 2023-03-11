using Microsoft.AspNetCore.Components;
using System;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;
public  class TrainMeetNote : TrainCallNote {
    
    public required int TrainNumber { get; init; }
    public required TrainInfo MeetingTrain { get; init; }
    public TimeSpan FromTime { get; init; }
    public TimeSpan ToTime { get; init; }
    
    public override MarkupString AsMarkup() => new(Markup);
    private string Markup => NoteDays.IsAnyOtherDays(ServiceOperationDays) ?
        string.Concat(LocalizedText.TextMarkup(), MeetingTrain.ToString().ItemMarkup(), Time.ItemMarkup()) :
        string.Empty;

    private string LocalizedText => TrainNumber.BothIsOddOrEven(MeetingTrain.Number) ? Resources.Notes.Passes : Resources.Notes.Meets;
    private string Time => $"{FromTime:hh\\:mm}-{ToTime:hh\\:mm}";
    private OperationDays NoteDays => MeetingTrain.OperationDays & ServiceOperationDays;
}
