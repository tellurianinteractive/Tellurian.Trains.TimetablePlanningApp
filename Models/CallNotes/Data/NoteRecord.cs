namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class NoteRecord
{
    public required int CallId { get; set; }
    public required byte TrainOperationDaysFlags { get; set; }
    public required byte DutyOperationDaysFlags { get; set; }
}
