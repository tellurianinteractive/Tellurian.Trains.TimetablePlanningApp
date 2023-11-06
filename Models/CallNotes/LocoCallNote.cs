  using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.CallNotes.Resources;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class LocoCallNote : TrainCallNote {
    public LocoCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
        IsToDispatcher = true;
        LocoOperationDays = OperationDays.Daily;
    }
    public OperationDays LocoOperationDays { get; init; }
    protected abstract string Item { get; }
    protected abstract string LocalizedText { get; }
    private string Text => LocalizedText.SpanText();
    private string Days => LocoOperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.SpanDays();
    protected virtual string Remark => string.Empty;
    private OperationDays NoteDays => LocoOperationDays & ServiceOperationDays;
    public override MarkupString Markup() => new(string.Concat(Days, Text, Item, Remark).Div());
}

public sealed class LocoConnectNote : LocoCallNote {
    public LocoConnectNote()
    {
        IsForDeparture = true;
    }
    public required LocoInfo LocoInfo { get; init; }
    protected override string Item => LocoInfo.ToString('.').SpanValue();
    public bool CollectFromStagingArea { get; init; }
    protected override string Remark => CollectFromStagingArea ? Resources.Notes.CollectLocoFromStagingArea.SpanText() : string.Empty;
    protected override string LocalizedText => Resources.Notes.ConnectLoco;
}

public sealed class LocoDisconnectNote : LocoCallNote {
    public LocoDisconnectNote()
    {
        IsForArrival = true;
    }
    public required LocoInfo LocoInfo { get; init; }
    public bool DriveToStagingArea { get; init; }
    protected override string Item => LocoInfo.ToString('.').SpanValue();
    protected override string Remark =>
        DriveToStagingArea ? DriveToStagingAreaText :
        string.Empty;

    protected override string LocalizedText => Resources.Notes.DisconnectLoco;
    private static string DriveToStagingAreaText => Resources.Notes.DriveToStagingArea.SpanText();
}

public sealed class LocoExchangeNote : LocoCallNote {
    public LocoExchangeNote()
    {
        IsForArrival = true;
    }
    public required OperationDays ReplacingLocoOperationDays { get; init; }
    protected override string LocalizedText => throw new System.NotImplementedException();

    protected override string Item => string.Empty;
}

public sealed class LocoTurnOrCirculateCallNote : TrainCallNote
{
    // NOTE: Filtering double direction is made in the mappings.
    public LocoTurnOrCirculateCallNote()
    {
        IsForArrival = true;
        IsToDispatcher = true;
        IsToLocoDriver = true;
    }
    public required OperationDays LocoOperationDays { get; init; }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }

    public override MarkupString Markup() => new(string.Concat(Days, MarkupText).Div());

    private string MarkupText =>
        $"""
        <span class="callnote text">{LocalizedText}</span>
        """;


    
    private string Days => LocoOperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.SpanDays();
    private OperationDays NoteDays => LocoOperationDays & ServiceOperationDays;

    private string LocalizedText => 
        CirculateLoco && TurnLoco ? Notes.TurnAndReverseLoco: 
        CirculateLoco ? Notes.CirculateLoco : 
        TurnLoco ? Notes.TurnLoco:
        string.Empty;

}




