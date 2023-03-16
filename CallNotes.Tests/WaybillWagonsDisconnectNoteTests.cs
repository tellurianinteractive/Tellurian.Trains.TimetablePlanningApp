using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsDisconnectNoteTests
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
        var notes = await Notes(91);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Disconnect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span> </span></div>
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
            <span class="note-text">Disconnect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span> </span></div><div class="note-item"><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span> </span></div>
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
            <span class="note-text">Disconnect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span> </span></div>
            """; ;

        const string expected2 = """
            <span class="note-text">Disconnect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span> </span></div>
            """;

        Assert.AreEqual(1, note1.ForCallId);
        Assert.AreEqual(new MarkupString(extected1), note1.Markup());
        Assert.AreEqual(2, note2.ForCallId);
        Assert.AreEqual(new MarkupString(expected2), note2.Markup());

    }
}

