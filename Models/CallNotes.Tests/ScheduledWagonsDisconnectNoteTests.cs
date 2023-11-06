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

            <div class="callnote text">Disconnect wagon groups </div>
            <div class="callnote item"><span class="callnote days">Mo,We,Fr: </span><span class="callnote value">turnus 21 (max 2 wagons) </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
