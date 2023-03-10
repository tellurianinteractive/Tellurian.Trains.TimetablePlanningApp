using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class BlockDisconnectNotesTests
{
    private ICallEventsService? CallEventsService;
    [TestInitialize]
    public void TestInitialize()
    {
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<BlockDisconnectNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetBlockDisconnectEventsAsync(testCase).ConfigureAwait(false);
        return events.AsBlockDisconnectNotes();
    }


    [TestMethod]
    public async Task SingleDestination()
    {
        var notes = await Notes(1);

        const string expected = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), notes.First().AsMarkup());
    }

    [TestMethod]
    public async Task MultipleDestination()
    {
        var notes = await Notes(2);

        const string expected = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), notes.First().AsMarkup());
    }

    [TestMethod]
    public async Task TwoCallDestination()
    {
        var notes = await Notes(3);

        const string expectedFirst = "";

        const string expectedLast = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span>
            """;

        Assert.AreEqual(new MarkupString(expectedFirst), notes.First().AsMarkup());
        Assert.AreEqual(new MarkupString(expectedLast), notes.Last().AsMarkup());

    }
}

