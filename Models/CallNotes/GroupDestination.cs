using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class GroupDestination(int displayOrder, OperationDays operationDays, string name, int positionInTrain = 0,  int maxNumberOfWagons = 0)
{
    public int DisplayOrder { get; init; } = displayOrder;
    public string Name { get; init; } = name;
    public int PositionInTrain { get; init; } = positionInTrain;
    public int MaxNumberOfWagons { get; init; } = maxNumberOfWagons;
    public  OperationDays OperationDays { get; init; } = operationDays;

}

public sealed class CountryDestination(string name, OperationDays operationDays, string? flagHref, int positionInTrain, int maxNumberOfWagons) : GroupDestination(1, operationDays, name, positionInTrain, maxNumberOfWagons)
{
    public CountryDestination(WagonGroupConnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.FlagHref, record.PositionInTrain, record.MaxNumberOfWagons) { }
    public CountryDestination(WagonGroupDisconnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.FlagHref, record.PositionInTrain, 0) { }
    internal readonly string? FlagHref = flagHref;

}

public sealed class RegionDestination(string name, OperationDays operationDays, string backColor, int positionInTrain, int maxNumberOfWagons) : GroupDestination(2, operationDays, name, positionInTrain, maxNumberOfWagons)
{
    public RegionDestination(WagonGroupConnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.DestinationBackColor!, record.PositionInTrain, record.MaxNumberOfWagons) { }
    public RegionDestination(WagonGroupDisconnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.DestinationBackColor!, record.PositionInTrain, 0) { }
    internal string BackColor { get; init; } = backColor;
    internal string ForeColor => BackColor.TextColor();
}

public sealed class CargoDestination(string name, OperationDays operationDays, int positionInTrain, int maxNumberOfWagons) : GroupDestination(3, operationDays, name, positionInTrain, maxNumberOfWagons) {
    public CargoDestination(WagonGroupConnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.PositionInTrain, record.MaxNumberOfWagons) { }
    public CargoDestination(WagonGroupDisconnectRecord record) : this(record.DestinationName, record.OperationDaysFlags.ToOperationDays(), record.PositionInTrain, 0) { }
}

public sealed class TransferDestination(string name, OperationDays operationDays, int positionInTrain, int maxNumberOfWagons) : GroupDestination(4, operationDays, name, positionInTrain, maxNumberOfWagons) { 
    public TransferDestination(WagonGroupConnectRecord record) : this(record.TransferDestinationName!, record.OperationDaysFlags.ToOperationDays(), record.PositionInTrain, record.MaxNumberOfWagons) { }
    public TransferDestination(WagonGroupDisconnectRecord record) : this(record.TransferDestinationName!, record.OperationDaysFlags.ToOperationDays(), record.PositionInTrain, 0) { }
}

public record Image(string MimeType, byte[] Data);

internal static class GroupDestinationExtensions
{
    public static GroupDestination ToGroupDestination(this WagonGroupConnectRecord record) =>
        record.TransferDestinationName.HasValue() ? new TransferDestination(record) :
        record.FlagHref is not null && record.CountryDomain.HasValue() ? new CountryDestination(record) :
        record.DestinationBackColor.IsNoneWhiteColor() ? new RegionDestination(record) :
        new CargoDestination(record);

    public static GroupDestination ToGroupDestination(this WagonGroupDisconnectRecord record) =>
        record.TransferDestinationName.HasValue() ? new TransferDestination(record) :
        record.FlagHref is not null && record.CountryDomain.HasValue() ? new CountryDestination(record) :
        record.DestinationBackColor.IsNoneWhiteColor() ? new RegionDestination(record) :
        new CargoDestination(record);

}

public static class GroupDestinationMarkup
{
    public static MarkupString ToMarkup(this GroupDestination destination, OperationDays scheduledDays, bool alwaysShowOperationDays = false) =>
        new(destination.ToMarkupText(scheduledDays, alwaysShowOperationDays));

    private static string ToMarkupText(this GroupDestination destination, OperationDays scheduledDays, bool alwaysShowOperationDays = false) =>
         MarkupExtensions.Div(
             alwaysShowOperationDays ? string.Concat(destination.OperationDays.ShortName.SpanDays(), destination.ToMarkupText()) :
             destination.OperationDays.IsAllDaysOf(scheduledDays) ? destination.ToMarkupText() :
             destination.OperationDays.IsAnyDaysOf(scheduledDays) ? string.Concat(destination.OperationDays.ShortName.SpanDays(), destination.ToMarkupText()) :
             string.Empty,
             "item");


    private static string ToMarkupText(this GroupDestination destination) =>
        destination is CountryDestination ? Span("country", destination.Name) :
        destination is RegionDestination r ? Span("region", destination.Name, r.BackColor) :
        destination is TransferDestination ? Span("transfer", destination.Name) :
        Span("other", destination.Name);


    private static string Span(string cssClass, string value, string? backColor = null) =>
        backColor is null ?
        $"""
        <span class="callnote destination {cssClass}">{value}</span>
        """ :
        $"""
        <span class="callnote destination {cssClass}" style="background-color: {backColor}; color: {backColor.TextColor()}">{value}</span>
        """;
}

 