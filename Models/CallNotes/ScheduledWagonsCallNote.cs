using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class ScheduledWagonsCallNote : TrainCallNote {

    public ScheduledWagonsCallNote() {
        IsToDispatcher = true;
        IsToLocoDriver = true;
        IsToShunter = true;
    }
    private IEnumerable<ScheduledWagonsInfo> _wagons = Enumerable.Empty<ScheduledWagonsInfo>();
    public IEnumerable<ScheduledWagonsInfo> Wagons {
        get {
            return _wagons.OrderBy(b => b.PositionInTrain);
        }
        set {
            _wagons = value;
        }
    }
    public override MarkupString Markup() => new(MarkupString.Div());
    private string MarkupString => $"{LocalizedText.DivText()}{string.Join("", Wagons.Select(w => w.Markup(OperationDays, ShowAllOperationDays)))}";
    protected bool HasBlocks => _wagons.Any();
    private bool ShowAllOperationDays => _wagons.Any(w => !w.OperationDays.IsAllOtherDays(OperationDays));

    protected abstract string LocalizedText { get; }
    protected override OperationDays NoteDays => OperationDays.Daily;
}

public sealed class ScheduledWagonsConnectNote : ScheduledWagonsCallNote {
    public ScheduledWagonsConnectNote() { IsForDeparture = true; }

    protected override string LocalizedText => Resources.Notes.ConnectWagonGroups;
}
public sealed class ScheduledWagonsDisconnectNote : ScheduledWagonsCallNote {
    public ScheduledWagonsDisconnectNote() { IsForArrival = true; }

    protected override string LocalizedText => Resources.Notes.DisconnectWagonGroups;
}
