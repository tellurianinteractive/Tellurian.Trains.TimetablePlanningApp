namespace TimetablePlanning.Models.CallNotes;

public sealed class OriginInfo
{
    public required string FullName { get; init; }
    public string Markup => $"""
        <span class="note-origin">{FullName}</span>
        """;


}
