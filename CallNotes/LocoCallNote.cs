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
    }

    public required LocoInfo LocoInfo { get; init; }

    protected string Text => LocalizedText.TextMarkup();
    protected string Item => LocoInfo.ToString('.').ItemMarkup();
    protected string Days => LocoInfo.OperationDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty : NoteDays.ShortName.DaysMarkup();
    protected virtual string Remark => string.Empty;
    private OperationDays NoteDays => LocoInfo.OperationDays & ServiceOperationDays;
    protected abstract string LocalizedText { get; }
    public override MarkupString AsMarkup() => new(string.Concat(Days, Text, Item, Remark));
}

public class LocoConnectNote : LocoCallNote {
    public LocoConnectNote()
    {
        IsForDeparture = true;
    }
    public bool CollectFromStagingArea { get; init; }
    protected override string Remark => CollectFromStagingArea ? Resources.Notes.CollectLocoFromStagingArea.TextMarkup() : string.Empty;
    protected override string LocalizedText => Resources.Notes.ConnectLoco;
}

public class LocoDisconnectNote : LocoCallNote {
    public LocoDisconnectNote()
    {
        IsForArrival = true;
    }
    public bool CirculateLoco { get; init; }
    public bool TurnLoco { get; init; }
    public bool DriveToStagingArea { get; init; }
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

    protected static string CirculateLocoText => Resources.Notes.CirculateLoco.TextMarkup();
    private static string TurnLocoText => Resources.Notes.TurnLoco.TextMarkup();
    private static string DriveToStagingAreaText => Resources.Notes.DriveToStagingArea.TextMarkup();
    protected override string LocalizedText => Resources.Notes.DisconnectLoco;
}





