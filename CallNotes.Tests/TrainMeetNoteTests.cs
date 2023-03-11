using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class TrainMeetNoteTests {

    private ICallEventsService? CallEventsService;

    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<TrainMeetNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetTrainMeetEventsAsync(testCase).ConfigureAwait(false);
        return events.AsTrainMeetNotes();
    }

    [TestMethod]
    public async Task MeetNote()
    {
        var notes = await Notes(1);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Meets </span><span class="note-item">SJ Gt 4001 </span><span class="note-item">12:10-12:15 </span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task PassingNote()
    {
        var notes = await Notes(2);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Passes </span><span class="note-item">SJ Gt 4001 </span><span class="note-item">12:10-12:15 </span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

}
