using TimetablePlanning.Models.CallNotes.Resources;

namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class WagonGroupRecord : WagonRecord
{
    public int DisplayOrder { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public bool OriginAndBefore { get; init; }
    public bool ToAllDestinations { get; init; }
    public bool DestinationAndBeyond { get; init; }
    public string? DestinationBackColor { get; init; }
    public string? FlagHref { get; init; }
    public string? CountryDomain { get; init; }
    public string? TransferDestinationName { get; init; }
}

public sealed class WagonGroupDisconnectRecord : WagonGroupRecord
{
}

public sealed class WagonGroupConnectRecord : WagonGroupRecord
{
}

public abstract class WagonGroupShuntingRecord : NoteRecord
{
    protected abstract string LocalizedText { get; }
}
public sealed class WagonGroupToCustomersRecord : WagonGroupShuntingRecord
{
    protected override string LocalizedText => Notes.DeliverWagonsToFreightCustomers;
}
public sealed class WagonGroupFromCustomersRecord : WagonGroupShuntingRecord
{
    protected override string LocalizedText => Notes.CollectWagonsFromFreightCustomers;
}
