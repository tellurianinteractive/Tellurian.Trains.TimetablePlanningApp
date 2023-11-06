using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public sealed class DestinationInfo
{
    public required string FullName { get; init; }
    public required string BackColor { get; init; }
    public string ForeColor => BackColor.TextColor();
}
