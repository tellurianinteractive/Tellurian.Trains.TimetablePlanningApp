using System.Data;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class ManualNoteRecordMapper
{
    public static ManualNoteRecord ToManualNoteRecord(this IDataRecord record) =>
        new() {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(ManualNoteRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(ManualNoteRecord.TrainOperationDaysFlags)),
            LocoOperationDaysFlags = record.GetByte(nameof(ManualNoteRecord.LocoOperationDaysFlags)),
            TwoLetterIsoLanguageName = record.GetString(nameof(ManualNoteRecord.TwoLetterIsoLanguageName)),
            Text = record.GetString(nameof(ManualNoteRecord.Text)),
        };
}
