using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class TrainMeetNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task MeetNote()
    {
        var notes = await Notes(71);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote text">Meets </span><span class="callnote value">SJ Gt 4001 </span><span class="callnote value">12:11-12:15 </span></div>
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
        var notes = await Notes(76);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote text">Passes </span><span class="callnote days">Mo,We,Fr: </span><span class="callnote value">SJ Gt 4001 </span><span class="callnote value">12:11-12:15 </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task PassingNoteWithSeveralPassingTrains()
    {
        var notes = await Notes(77);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote text">Passes </span><span class="callnote days">Mo,We,Fr: </span><span class="callnote value">BSX Gt 4001 </span><span class="callnote value">12:11-12:16 </span>, <span class="callnote days">Daily: </span><span class="callnote value">SJ Xt 501 </span><span class="callnote value">12:41-12:43 </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
