namespace TimetablePlanning.Models.CallNotes;

public sealed class WaybillWagonGroup
{
    public required int PositionInTrain { get; init; }
    public int MaxNumberOfWagons { get; init; }
    public required DestinationInfo Destination { get; init; }
    public required OriginInfo Origin { get; init; }
    public string Markup => MaxNumberOfWagons == 0 ? $"""
        <span class="note-destination" style="color: {Destination.ForeColor};background-color: {Destination.BackColor};">{Destination.FullName}</span>
        """ :
        $"""
        <span class="note-destination" style="color: {Destination.ForeColor};background-color: {Destination.BackColor};">{Destination.FullName} × {MaxNumberOfWagons}</span>
        """;
}
