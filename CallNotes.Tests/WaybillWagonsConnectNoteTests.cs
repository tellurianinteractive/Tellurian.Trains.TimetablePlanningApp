using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsConnectNoteTests {

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
    public async Task SingleNote()
    {
        var notes = await Notes(81);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Connect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span> </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task SeveralDestinations()
    {
        var notes = await Notes(82);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Connect wagons to </span> <div class="note-item"><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby × 6</span> </span></div><div class="note-item"><span class="note-value"><span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span> </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task DestinationsDifferentDays()
    {
        var notes = await Notes(83);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Connect wagons to </span> <div class="note-item"><span class="note-days">Tu,Th,Sa </span><span class="note-value"><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby × 6</span> </span></div><div class="note-item"><span class="note-days">Mo,We,Fr </span><span class="note-value"><span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span> </span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
