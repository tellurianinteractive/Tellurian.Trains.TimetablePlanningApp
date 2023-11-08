using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;
[TestClass]
public class ScheduledWagonsDisconnectNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task SingleDestination()
    {
        var notes = await Notes(51);
        var note = notes.First();

        const string expected = """

            <div class="callnote">
            <div class="callnote text">Disconnect wagon groups </div>
            <div class="callnote item"><span class="callnote days">Mo,We,Fr: </span><span class="callnote value">turnus 21 (max 2 wagons) </span></div></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
