using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.CallNotes.Resources;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class WagonGroupCallNote : TrainCallNote
{
    public WagonGroupCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
    }

    public IEnumerable<GroupDestination> GroupDestinations { get; internal set; } = Enumerable.Empty<GroupDestination>();

    public override MarkupString Markup() => new(MarkupText);
    private string MarkupText => 
        GroupDestinations.Any() ? $"{LocalizedText.DivText()}{string.Join("", GroupDestinationsMarkup)}" : 
        string.Empty;
    private IEnumerable<MarkupString> GroupDestinationsMarkup => 
        GroupDestinations
            .OrderBy(d => d.PositionInTrain)
            .ThenBy(d => d.Name)
            .Select(d => d.ToMarkup(OperationDays));

    protected abstract string LocalizedText { get; }
    protected override OperationDays NoteDays => OperationDays.Daily;

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

public abstract class WagonGroupSwitchOrCollectCallNote : TrainCallNote
{
    public WagonGroupSwitchOrCollectCallNote()
    {
        IsToLocoDriver = true;
        IsToShunter = true;
    }
    public override MarkupString Markup() => throw new NotImplementedException();
    protected abstract string LocalizedText { get; }
    protected override OperationDays NoteDays => OperationDays.Daily;

}

public sealed class WagonGroupSwitchCallNote : WagonGroupSwitchOrCollectCallNote
{
    public WagonGroupSwitchCallNote()
    {
        IsForArrival=true;
    }
    protected override string LocalizedText => Notes.SwitchWagonsToFreightCustomers;
}
public sealed class WagonGroupCollectCallNote : WagonGroupSwitchOrCollectCallNote
{
    public WagonGroupCollectCallNote()
    {
        IsForDeparture = true;
    }
    protected override string LocalizedText => Notes.CollectWagonsFromFreightCustomers;

}

