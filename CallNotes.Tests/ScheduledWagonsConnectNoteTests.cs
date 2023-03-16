using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            <span class="note-text">Connect wagon groups </span><div class="note-item"><span class="note-value">turnus 22 </span></div>
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
            <span class="note-text">Connect wagon groups </span><div class="note-item"><span class="note-days">Mo,We,Fr </span><span class="note-value">turnus 21 </span></div><div class="note-item"><span class="note-days">Daily </span><span class="note-value">turnus 22 </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
