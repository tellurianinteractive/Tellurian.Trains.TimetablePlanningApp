using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public abstract class NoteTestsBase
{
    protected CallNotesService? CallNotesService;

    [TestInitialize]
    public void TestInitialize()
    {
        "en-UK".IsTestLanguage();
        CallNotesService = new CallNotesService(new TestCallNoteRecordsService());
    }

    protected async Task<IEnumerable<TrainCallNote>> Notes(int testCase) =>
        await CallNotesService!.GetCallNotesAsync(testCase).ConfigureAwait(false);
}
