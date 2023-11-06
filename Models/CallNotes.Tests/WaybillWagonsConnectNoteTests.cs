using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsConnectNoteTests: NoteTestsBase
{
    [TestMethod]
    public async Task SingleNote()
    {
        var notes = await Notes(81);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task SeveralDestinations()
    {
        var notes = await Notes(82);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote destination other">Ytterby</span></div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task DestinationsDifferentDays()
    {
        var notes = await Notes(83);
        var note = notes.First();

        const string expected = """
            
            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote days">Tu,Th,Sa: </span><span class="callnote destination other">Ytterby</span></div>
            <div class="callnote item"><span class="callnote days">Mo,We,Fr: </span><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
