using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class WaybillWagonGroupCallNote : TrainCallNote {
    public WaybillWagonGroupCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
    }
    public IEnumerable<WaybillWagonGroup> Blocks {
        get {
            return _blocks.OrderBy(b => b.PositionInTrain);
        }
        internal set {
            _blocks = value;
        }
    }
    protected bool HasBlocks => _blocks.Any();
    private IEnumerable<WaybillWagonGroup> _blocks = Enumerable.Empty<WaybillWagonGroup>();

    public override MarkupString AsMarkup() => new(Markup);
    protected string Markup => HasBlocks ? $"{LocalizedText.TextMarkup()} {string.Join("", Blocks.Select(d => d.Markup))}" : string.Empty;
    protected abstract string LocalizedText { get; }

}

public sealed class WaybillWagonsConnectNote : WaybillWagonGroupCallNote {
    public WaybillWagonsConnectNote()
    {
        IsForDeparture = true;
    }
    protected override string LocalizedText => Resources.Notes.ConnectWagonsTo;

}
public sealed class WaybillWagonsDisconnectNote : WaybillWagonGroupCallNote {
    public WaybillWagonsDisconnectNote()
    {
        IsForArrival = true;
    }
    protected override string LocalizedText => Resources.Notes.DisconnectWagonsTo;
}
