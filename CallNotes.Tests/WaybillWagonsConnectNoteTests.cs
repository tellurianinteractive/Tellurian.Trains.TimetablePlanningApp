using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsConnectNoteTests {

    private ICallEventsService? CallEventsService;

    [TestInitialize]
    public void TestInitialize()
    {
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<WaybillWagonsConnectNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetBlockConnectEventsAsync(testCase).ConfigureAwait(false);
        return events.AsBlockConnectNotes();
    }

    [TestMethod]
    public async Task SingleNote()
    {
        var notes = await Notes(1);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Connect wagons to </span> <span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task SeveralDestinations()
    {
        var notes = await Notes(2);
        var note = notes.First();

        const string expected = """
            <span class="note-text">Connect wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby × 6</span><span class="note-destination" style="color: #FFFFFF;background-color: #009933;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());

    }
}
