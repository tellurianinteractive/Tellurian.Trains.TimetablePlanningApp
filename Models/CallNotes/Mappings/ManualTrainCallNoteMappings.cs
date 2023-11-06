using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;
internal static class ManualTrainCallNoteMappings
{
    public static IEnumerable<ManualTrainCallNote> ToManualTrainCallNotes(this IEnumerable<ManualNoteRecord> records) =>
        records.GroupBy(r => r.CallId).Select(g => g.ToManualTrainCallNote());


    /// <summary>
    /// Creates a <see cref="ManualTrainCallNote"/> from <see cref="ManualNoteRecord">note records</see> for the same callid.
    /// </summary>
    /// <param name="records">This is the texts in different langauges for the call id.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    internal static ManualTrainCallNote ToManualTrainCallNote(this IEnumerable<ManualNoteRecord> records)
    {
        if (records is null || !records.Any()) throw new ArgumentNullException(nameof(records));
        if (records.Count() > 1 && records.GroupBy(r => r.CallId).Count()>1) throw new InvalidDataException($"{nameof(records)} contains more than one call id.");
        var first = records.First();
        var note = new ManualTrainCallNote()
        {
            ForCallId=first.CallId,
            DutyOperationDays = first.DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = first.TrainOperatingDaysFlags.ToOperationDays(),
            IsForArrival = first.IsForArrival,
            IsForDeparture = first.IsForDeparture,
            IsToDispatcher = first.IsToDispatcher,
            IsToLocoDriver = first.IsToLocoDriver,
            IsToShunter = first.IsToShunter,
        };
        note.AddTexts(records);
        return note;

    }

    private static void AddTexts(this ManualTrainCallNote note, IEnumerable<ManualNoteRecord> records) {
        foreach (var record in records)
        {
            note.Add( record.Text, record.TwoLetterIsoLanguageName);
        }
    }
}
