using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoDisconnectionNoteTests: NoteTestsBase
{
    [TestMethod]
    public async Task NoteWithDays()
    {
        var notes = await Notes(21);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote days">Tu,Th,Sa,Su: </span><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ Rc6 turnus 2. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());

        // These need only to be tested once
        Assert.IsTrue(note.IsForArrival);
        Assert.IsFalse(note.IsForDeparture);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsTrue(note.IsToDispatcher);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task NoteWithOtherDays()
    {
        var notes = await Notes(22);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote days">Mo,We,Fr: </span><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ Rc6 turnus 1. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
