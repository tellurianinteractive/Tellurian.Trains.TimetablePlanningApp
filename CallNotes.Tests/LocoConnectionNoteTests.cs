using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoConnectionNoteTests
{
    private ICallEventsService? CallEventsService;
   

    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<LocoConnectNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetLocoConnectEventsAsync(testCase).ConfigureAwait(false);
        return events.AsLocoConnectNotes();
    }

    [TestMethod]
    public async Task NoteWithDays()
    {
        var notes = await Notes(1);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Tu,Th,Sa,Su </span><span class="note-text">Connect loco </span><span class="note-item">SJ Rc6 turn 2. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
        
        // These need only to be tested once
        Assert.IsFalse(note.IsForArrival);
        Assert.IsTrue(note.IsForDeparture);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsTrue(note.IsToDispatecher);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task NoteWithOtherDays()
    {
        var notes = await Notes(2);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Mo,We,Fr </span><span class="note-text">Connect loco </span><span class="note-item">SJ Rc6 turn 1. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndCollectRemark()
    {
        var notes = await Notes(3);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Connect loco </span><span class="note-item">SJ T44 232 turn 3. </span><span class="note-text">Collect loco from staging area. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
