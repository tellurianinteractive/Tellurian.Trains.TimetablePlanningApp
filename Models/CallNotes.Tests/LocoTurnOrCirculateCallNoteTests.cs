using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoTurnOrCirculateCallNoteTests : NoteTestsBase
{

    [TestMethod]
    public async Task OnlyCirculate()
    {
        var notes = await Notes(24);

        var expected = 
            $"""

            <div class="callnote"><span class="callnote text">Circulate loco.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task OnlyTurn()
    {
        var notes = await Notes(25);

        var expected =
            $"""

            <div class="callnote"><span class="callnote text">Turn loco.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task TurnAndCirculate()
    {
        var notes = await Notes(26);

        var expected =
            $"""

            <div class="callnote"><span class="callnote text">Turn and circulate loco.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task TurnDoubleDirectedGivesNoNote()
    {
        var notes = await Notes(27);
        Assert.AreEqual(0, notes.Count());

    }

    [TestMethod]
    public async Task TurnOnlySomeDays()
    {
        var notes = await Notes(28);
        var expected =
            $"""

            <div class="callnote"><span class="callnote days">Mo,We,Fr: </span><span class="callnote text">Turn and circulate loco.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }


    [TestMethod]
    public async Task TurnAllDaysOfDuty()
    {
        var notes = await Notes(29);
        var expected =
            $"""

            <div class="callnote"><span class="callnote text">Turn and circulate loco.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }
}
