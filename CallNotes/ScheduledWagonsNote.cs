using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class ScheduledWagonsNote : TrainCallNote {
    private IEnumerable<ScheduledWagonsInfo> _wagons = Enumerable.Empty<ScheduledWagonsInfo>();
    public IEnumerable<ScheduledWagonsInfo> Wagons {
        get {
            return _wagons.OrderBy(b => b.PositionInTrain);
        }
        internal set {
            _wagons = value;
        }
    }
    public override MarkupString AsMarkup() => new(string.Concat(Label, string.Join("",Wagons.Select(w => w.Markup(ServiceOperationDays, ShowAllOperationDays)))));
    protected bool HasBlocks => _wagons.Any();
    private bool ShowAllOperationDays => _wagons.Any(w => !w.OperationDays.IsAllOtherDays(ServiceOperationDays));

    protected abstract string Label { get; }
}

public sealed class ScheduledWagonsConnectNote : ScheduledWagonsNote {

    protected override string Label => Resources.Notes.ConnectWagonGroups.SpanText();
}
public sealed class ScheduledWagonsDisconnectNote : ScheduledWagonsNote {

    protected override string Label => Resources.Notes.DisconnectWagonGroups.SpanText();
}
