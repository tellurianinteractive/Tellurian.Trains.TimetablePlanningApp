namespace TimetablePlanning.Models.CallNotes.Data;

public class ManualNoteRecord : NoteRecord
{
    public required byte LocoOperationDaysFlags { get; init; }
    public required string TwoLetterIsoLanguageName { get; init; }
    public required string Text { get; init; }
    public bool IsForArrival {  get; init; }
    public bool IsForDeparture { get; init; }
    public bool IsToLocoDriver { get; init; }
    public bool IsToShunter { get; init; }
    public bool IsToDispatcher { get; init; }
}
