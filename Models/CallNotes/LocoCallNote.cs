  using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class LocoCallNote : TrainCallNote {
    public LocoCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
        IsToDispatcher = true;
        LocoOperationDays = OperationDayFlags.Daily.AsOperationDays();
    }
    public OperationDays LocoOperationDays { get; init; }
    protected abstract string Item { get; }
    protected abstract string LocalizedText { get; }
    private string Text => LocalizedText.SpanText();
    private string Days => LocoOperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.SpanDays();
    protected virtual string Remark => string.Empty;
    private OperationDays NoteDays => LocoOperationDays & ServiceOperationDays;
    public override MarkupString Markup() => new(string.Concat(Days, Text, Item, Remark));
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
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
    public bool DriveToStagingArea { get; init; }
    protected override string Item => LocoInfo.ToString('.').SpanValue();
    protected override string Remark =>
        LocoInfo.IsDoubleDirectionTrain && DriveToStagingArea ? DriveToStagingAreaText :
        LocoInfo.IsSingleDirectionTractionUnit ?
            DriveToStagingArea && TurnLoco && CirculateLoco ? string.Concat(DriveToStagingAreaText, TurnLocoText, CirculateLocoText) :
            DriveToStagingArea && TurnLoco ? string.Concat(DriveToStagingAreaText, TurnLocoText) :
            TurnLoco && CirculateLoco ? string.Concat(TurnLocoText, CirculateLocoText) :
            CirculateLoco ? CirculateLocoText :
            TurnLoco ? TurnLocoText :
            DriveToStagingArea ? DriveToStagingAreaText :
            string.Empty :
        string.Empty;

    protected override string LocalizedText => Resources.Notes.DisconnectLoco;
    private static string CirculateLocoText => Resources.Notes.CirculateLoco.SpanText();
    private static string TurnLocoText => Resources.Notes.TurnLoco.SpanText();
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




