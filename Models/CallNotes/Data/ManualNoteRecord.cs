namespace TimetablePlanning.Models.CallNotes.Data;

public class ManualNoteRecord : NoteRecord
{
    public required byte DisplayedDaysFlag { get; init; }
    public required string TwoLetterIsoLanguageName { get; init; }
    public required string Text { get; init; }
}
