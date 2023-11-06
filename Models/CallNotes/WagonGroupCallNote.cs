using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class WagonGroupCallNote : TrainCallNote
{
    public WagonGroupCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
    }

    public IEnumerable<GroupDestination> GroupsDestinations { get { return _groupDestinations; } internal set { _groupDestinations = value; } }
    private IEnumerable<GroupDestination> _groupDestinations = Enumerable.Empty<GroupDestination>();

    public override MarkupString Markup() => new(MarkupText);
    private string MarkupText => 
        _groupDestinations.Any() ? $"{LocalizedText.DivText()}{string.Join("", _groupDestinations.OrderBy(d => d.PositionInTrain).Select(d => d.ToMarkup(ServiceOperationDays)))}" : 
        string.Empty;

    protected abstract string LocalizedText { get; }

}

public sealed class WagonGroupConnectNote : WagonGroupCallNote
{
    public WagonGroupConnectNote()
    {
        IsForDeparture = true;
    }
    protected override string LocalizedText => Resources.Notes.ConnectWagonsTo;

}
public sealed class WagonGroupDisconnectNote : WagonGroupCallNote
{
    public WagonGroupDisconnectNote()
    {
        IsForArrival = true;
    }
    protected override string LocalizedText => Resources.Notes.DisconnectWagonsTo;
}

