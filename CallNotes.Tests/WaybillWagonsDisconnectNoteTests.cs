using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsDisconnectNoteTests
{
    private ICallEventsService? CallEventsService;

    [TestInitialize]
    public void TestInitialize()
    {
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<WaybillWagonsDisconnectNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetBlockDisconnectEventsAsync(testCase).ConfigureAwait(false);
        return events.AsBlockDisconnectNotes();
    }


    [TestMethod]
    public async Task SingleDestination()
    {
        var notes = await Notes(1);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Disconnect wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());

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
        var notes = await Notes(2);

        const string expected = """
            <span class="note-text">Disconnect wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), notes.First().AsMarkup());
    }

    [TestMethod]
    public async Task TwoCallDestination()
    {
        var notes = await Notes(3);
        var note1 = notes.First();
        var note2 = notes.Last();

        const string extected1 = """
            <span class="note-text">Disconnect wagons to </span> <span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span>
            """; ;

        const string expected2 = """
            <span class="note-text">Disconnect wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span>
            """;

        Assert.AreEqual(1, note1.ForCallId);
        Assert.AreEqual(new MarkupString(extected1), note1.AsMarkup());
        Assert.AreEqual(2, note2.ForCallId);
        Assert.AreEqual(new MarkupString(expected2), note2.AsMarkup());

    }
}

