using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes;

public abstract class LocoCallNote : TrainCallNote
{
    public required LocoInfo LocoInfo { get; init; }

    protected string Text => TextMarkup(ResourceText);
    protected string Item => ItemMarkup(LocoInfo.ToString() + Dot);
    protected string Days => LocoInfo.OperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : DaysMarkup(NoteDays.ShortName);
    private OperationDays NoteDays => LocoInfo.OperationDays & ServiceOperationDays;
    protected abstract string ResourceText { get; }
}

public class LocoConnectNote : LocoCallNote
{
    public bool CollectFromStagingArea { get; init; }
    private string CollectText => CollectFromStagingArea ? TextMarkup(Resources.Notes.CollectLocoFromStagingArea) : string.Empty;
    protected override string ResourceText => Resources.Notes.ConnectLoco;
    public override MarkupString AsMarkup() => new(string.Concat(Days, Text, Item, CollectText));
}

public class LocoDisconnectNote : LocoCallNote
{
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
    public bool DriveToStagingArea { get; init; }
    private string RemarkText =>
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

    private string CirculateLocoText => TextMarkup(Resources.Notes.CirculateLoco);
    private string TurnLocoText => TextMarkup(Resources.Notes.TurnLoco);
    private string DriveToStagingAreaText => TextMarkup(Resources.Notes.DriveToStagingArea);

    protected override string ResourceText => Resources.Notes.DisconnectLoco;
    public override MarkupString AsMarkup() => new(string.Concat(Days, Text, Item, RemarkText));
}





