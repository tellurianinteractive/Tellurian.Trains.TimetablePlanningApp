using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;
[TestClass]
public class ScheduledWagonsDisconnectNoteTests
{

    private CallNotesService? CallNotesService;

    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallNotesService = new CallNotesService(new TestCallEventsService());
    }

    private async Task<IEnumerable<TrainCallNote>> Notes(int testCase) =>
        await CallNotesService!.GetCallNotesAsync(testCase).ConfigureAwait(false);

    [TestMethod]
    public async Task SingleDestination()
    {
        var notes = await Notes(51);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Disconnect wagon groups </span><div class="note-item"><span class="note-days">Mo,We,Fr: </span><span class="note-value">turnus 21 (max 2 wagons) </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
