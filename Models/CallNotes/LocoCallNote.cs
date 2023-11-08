using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.CallNotes.Resources;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class LocoCallNote : TrainCallNote
{
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
    protected virtual string Remark => string.Empty;
    protected override OperationDays NoteDays => LocoOperationDays;
    public override MarkupString Markup() => new(string.Concat(Days.SpanDays(), LocalizedText.SpanText(), Item.SpanValue(), Remark.SpanText()).Div());
}

public sealed class LocoConnectCallNote : LocoCallNote
{
    public LocoConnectCallNote()
    {
        IsForDeparture = true;
    }
    public required LocoInfo LocoInfo { get; init; }
    protected override string Item => LocoInfo.ToString('.');
    public bool CollectFromStagingArea { get; init; }
    protected override string Remark => CollectFromStagingArea ? Notes.CollectLocoFromStagingArea : string.Empty;
    protected override string LocalizedText => Notes.ConnectLoco;
}

public sealed class LocoDisconnectCallNote : LocoCallNote
{
    public LocoDisconnectCallNote()
    {
        IsForArrival = true;
    }
    public required LocoInfo LocoInfo { get; init; }
    public bool DriveToStagingArea { get; init; }
    protected override string Item => LocoInfo.ToString('.');
    protected override string Remark =>
        DriveToStagingArea ? DriveToStagingAreaText :
        string.Empty;

    protected override string LocalizedText => Notes.DisconnectLoco;
    private static string DriveToStagingAreaText => Notes.DriveToStagingArea;
}

public sealed class LocoExchangeCallNote : LocoCallNote
{
    public LocoExchangeCallNote()
    {
        IsForArrival = true;
    }
    public required OperationDays ReplacingLocoOperationDays { get; init; }
    public required string ReplacingLocoOperatorSignature { get; init; }
    public required string ReplacingLocoClass { get; init; }
    public string? ReplacingLocoNumber { get; init; }
    protected override string LocalizedText => $"{Notes.ReplaceLoco} {Notes.UseLoco}";

    protected override string Item =>
        $"{ReplacingLocoOperatorSignature} {ReplacingLocoClass} {ReplacingLocoNumber}".TrimEnd();
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
    protected override OperationDays NoteDays => LocoOperationDays;
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }

    public override MarkupString Markup() => new(string.Concat(Days.SpanDays(), MarkupText).Div());

    private string MarkupText =>
        $"""
        <span class="callnote text">{LocalizedText}</span>
        """;

    private string LocalizedText =>
        CirculateLoco && TurnLoco ? Notes.TurnAndReverseLoco :
        CirculateLoco ? Notes.CirculateLoco :
        TurnLoco ? Notes.TurnLoco :
        string.Empty;

}