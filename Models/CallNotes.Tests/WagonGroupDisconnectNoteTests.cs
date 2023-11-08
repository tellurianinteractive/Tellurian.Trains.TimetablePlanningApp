using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WagonGroupDisconnectNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task SingleDestination()
    {
        var notes = await Notes(91);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Disconnect wagons to </div>
            <div class="callnote item"><span class="callnote destination other">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());

        // Test only once
        Assert.IsTrue(note.IsForArrival);
        Assert.IsFalse(note.IsForDeparture);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsFalse(note.IsToDispatcher);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task MultipleDestination()
    {
        var notes = await Notes(92);

        const string expected = """

            <div class="callnote text">Disconnect wagons to </div>
            <div class="callnote item"><span class="callnote destination other">Ytterby</span></div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task TwoCallDestination()
    {
        var notes = await Notes(93);
        var note1 = notes.First();
        var note2 = notes.Last();

        const string extected1 = """

            <div class="callnote text">Disconnect wagons to </div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """; ;

        const string expected2 = """

            <div class="callnote text">Disconnect wagons to </div>
            <div class="callnote item"><span class="callnote destination other">Ytterby</span></div>
            """;

        Assert.AreEqual(1, note1.ForCallId);
        Assert.AreEqual(new MarkupString(extected1), note1.Markup());
        Assert.AreEqual(2, note2.ForCallId);
        Assert.AreEqual(new MarkupString(expected2), note2.Markup());

    }
}

