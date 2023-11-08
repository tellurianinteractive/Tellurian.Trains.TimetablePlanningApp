using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoExchangeCallNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task LocoExchangedAllDays()
    {
        var notes = await Notes(31);

        var expected =
        $"""

        <div class="callnote"><span class="callnote text">Replace loco. Use loco </span><span class="callnote value">SJ Rc5 1422 </span></div>
        """;

        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task LocoExchangedSomeDays()
    {
        var notes = await Notes(32);

        var expected =
        $"""

        <div class="callnote"><span class="callnote days">Mo,We,Fr: </span><span class="callnote text">Replace loco. Use loco </span><span class="callnote value">SJ Rc5 1422 </span></div>
        """;

        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }
}
