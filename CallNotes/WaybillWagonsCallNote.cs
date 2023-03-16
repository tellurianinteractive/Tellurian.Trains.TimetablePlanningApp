using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class WaybillWagonsCallNote : TrainCallNote {
    public WaybillWagonsCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
    }

    private IEnumerable<WaybillWagonsInfo> _wagons = Enumerable.Empty<WaybillWagonsInfo>();
    public IEnumerable<WaybillWagonsInfo> Wagons {
        get {
            return _wagons.OrderBy(b => b.PositionInTrain);
        }
        internal set {
            _wagons = value;
        }
    }
    public override MarkupString Markup() => new(MarkupText);
    private string MarkupText => HasWagons ? $"{LocalizedText.SpanText()} {string.Join("", Wagons.Select(d => d.Markup(ServiceOperationDays, ShowAllOperationDays)))}" : string.Empty;
    private bool HasWagons => _wagons.Any();
    private bool ShowAllOperationDays => _wagons.Any(w => !w.OperationDays.IsAllOtherDays(ServiceOperationDays));
    protected abstract string LocalizedText { get; }
}

public sealed class WaybillWagonsConnectNote : WaybillWagonsCallNote {
    public WaybillWagonsConnectNote()
    {
        IsForDeparture = true;
    }
    protected override string LocalizedText => Resources.Notes.ConnectWagonsTo;

}
public sealed class WaybillWagonsDisconnectNote : WaybillWagonsCallNote {
    public WaybillWagonsDisconnectNote()
    {
        IsForArrival = true;
    }
    protected override string LocalizedText => Resources.Notes.DisconnectWagonsTo;
}
