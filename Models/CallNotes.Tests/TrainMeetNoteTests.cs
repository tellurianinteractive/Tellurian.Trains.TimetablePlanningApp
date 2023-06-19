using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class TrainMeetNoteTests {

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
    public async Task MeetNote()
    {
        var notes = await Notes(71);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Meets </span><span class="note-value">SJ Gt 4001 </span><span class="note-value">12:11-12:15 </span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());

        Assert.IsTrue(note.IsForArrival);
        Assert.IsFalse(note.IsForDeparture);
        Assert.IsTrue(note.IsToDispatcher);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsFalse(note.IsToShunter);
    }

    [TestMethod]
    public async Task PassingNote()
    {
        var notes = await Notes(72);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Mo,We,Fr: </span><span class="note-text">Passes </span><span class="note-value">SJ Gt 4001 </span><span class="note-value">12:11-12:15 </span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

}
