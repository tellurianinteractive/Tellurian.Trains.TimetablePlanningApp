using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class LocoCallNote : TrainCallNote
{
    public required LocoInfo LocoInfo { get; init; }

    protected string Text => ResourceText.TextMarkup();
    protected string Item => LocoInfo.ToString('.').ItemMarkup();
    protected string Days => LocoInfo.OperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.DaysMarkup();
    protected virtual string Remark => string.Empty;
    private OperationDays NoteDays => LocoInfo.OperationDays & ServiceOperationDays;
    protected abstract string ResourceText { get; }
    public override MarkupString AsMarkup() => new(string.Concat(Days, Text, Item, Remark));
}

public class LocoConnectNote : LocoCallNote
{
    public LocoConnectNote()
    {
        IsForDeparture = true;
        IsToLocoDriver = true;
        IsToShunter = true;
        IsToDispatecher = true;
    }
    public bool CollectFromStagingArea { get; init; }
    protected override string Remark => CollectFromStagingArea ? Resources.Notes.CollectLocoFromStagingArea.TextMarkup() : string.Empty;
    protected override string ResourceText => Resources.Notes.ConnectLoco;
}

public class LocoDisconnectNote : LocoCallNote
{
    public LocoDisconnectNote()
    {
        IsForArrival = true;
        IsToLocoDriver = true;
        IsToShunter = true;
        IsToDispatecher = true;
    }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
    public bool DriveToStagingArea { get; init; }
    protected override string Remark=>
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

    protected static string CirculateLocoText => Resources.Notes.CirculateLoco.TextMarkup();
    private static string TurnLocoText => Resources.Notes.TurnLoco.TextMarkup();
    private static string DriveToStagingAreaText => Resources.Notes.DriveToStagingArea.TextMarkup();
    protected override string ResourceText => Resources.Notes.DisconnectLoco;
}





