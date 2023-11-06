using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class ManualTrainCallNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task SingleTextNote()
    {
        var notes = await Notes(61);
        const string expected =
            """

            <div class="callnote"><span class="callnote text">Manuell not på svenska.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());

    }

    [TestMethod]
    public async Task SingleTextNoteWithTranslationsDefault()
    {
        var notes = await Notes(62);
        const string expected =
            """

            <div class="callnote"><span class="callnote text">Manual note in english.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task SingleTextNoteWithTranslationsSwedish()
    {
       "sv-SE".IsTestLanguage();
        var notes = await Notes(62);
        const string expected =
            """

            <div class="callnote"><span class="callnote text">Manuell not på svenska.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }

    [TestMethod]
    public async Task SingleTextNoteWithTranslationsDanishMissing()
    {
        "da-DK".IsTestLanguage();
        var notes = await Notes(62);
        const string expected =
            """

            <div class="callnote"><span class="callnote text">Manual note in english.</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), notes.First().Markup());
    }
}
