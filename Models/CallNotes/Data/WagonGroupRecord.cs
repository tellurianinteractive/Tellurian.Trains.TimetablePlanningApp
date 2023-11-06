using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class WagonGroupRecord : WagonRecord
{
    public int DisplayOrder { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public bool OriginAndBefore { get; init; }
    public bool ToAllDestinations { get; init; }
    public bool DestinationAndBeyond { get; init; }
    public string? DestinationBackColor { get; init; }
    public Image? Flag { get; init; }
    public string? CountryDomain { get; init; }
    public string? TransferDestinationFullName { get; init; }
}

public sealed class WagonGroupDisconnectRecord : WagonGroupRecord
{
}

public sealed class WagonGroupConnectRecord : WagonGroupRecord
{
}

internal static class WagonGroupExtensions
{
    public static WagonGroupConnectNote ToWagonGroupConnectNote(this IEnumerable<WagonGroupConnectRecord> records) =>
         new()
         {
             ForCallId = records.First().CallId,
             DutyOperationDays = records.First().DutyOperatingDaysFlags.ToOperationDays(),
             TrainOperationDays = records.First().TrainOperatingDaysFlags.ToOperationDays(),
             GroupsDestinations = records.Select(r => r.ToGroupDestination()),
         };
}
