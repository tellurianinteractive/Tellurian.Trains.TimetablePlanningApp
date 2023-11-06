using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class ScheduledWagonsConnectNoteTests {

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
    public async Task SingleWagonNote()
    {
        var notes = await Notes(41);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagon groups </div>
            <div class="callnote item"><span class="callnote value">turnus 22 (max 2 wagons) </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());

        Assert.IsFalse(note.IsForArrival);
        Assert.IsTrue(note.IsForDeparture);
        Assert.IsTrue(note.IsToDispatcher);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task TwoWagonsAtSameCallNote()
    {
        var notes = await Notes(42);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagon groups </div>
            <div class="callnote item"><span class="callnote days">Mo,We,Fr: </span><span class="callnote value">turnus 21 (max 2 wagons) </span></div>
            <div class="callnote item"><span class="callnote days">Daily: </span><span class="callnote value">turnus 22 (max 4 wagons) </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
