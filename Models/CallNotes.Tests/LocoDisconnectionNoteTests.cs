using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoDisconnectionNoteTests
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
    public async Task NoteWithDays()
    {
        var notes = await Notes(21);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote days">Tu,Th,Sa,Su: </span><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ Rc6 turnus 2. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());

        // These need only to be tested once
        Assert.IsTrue(note.IsForArrival);
        Assert.IsFalse(note.IsForDeparture);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsTrue(note.IsToDispatcher);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task NoteWithOtherDays()
    {
        var notes = await Notes(22);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote days">Mo,We,Fr: </span><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ Rc6 turnus 1. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndDriveStoStagingAreaRemark()
    {
        var notes = await Notes(23);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 232 turnus 3. </span><span class="callnote text">Drive loco to staging area. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndTurnRemark()
    {
        var notes = await Notes(24);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 236 turnus 8. </span><span class="callnote text">Turn loco. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndCirculateRemark()
    {
        var notes = await Notes(25);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 236 turnus 8. </span><span class="callnote text">Circulate loco. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndBothTurnAndCirculateRemark()
    {
        var notes = await Notes(26);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 236 turnus 8. </span><span class="callnote text">Turn loco. </span><span class="callnote text">Circulate loco. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndBothDriveToStagingAreaAndTurnLocoRemarks()
    {
        var notes = await Notes(27);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 236 turnus 8. </span><span class="callnote text">Drive loco to staging area. </span><span class="callnote text">Turn loco. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithDriveToStagingAreaAndTurnLocoAndCirculateIsError()
    {
        var notes = await Notes(28);
        var note = notes.First();

        const string expected =
            """
            
            <div class="callnote"><span class="callnote text">Disconnect loco </span><span class="callnote value">SJ T44 236 turnus 8. </span><span class="callnote text">Drive loco to staging area. </span><span class="callnote text">Turn loco. </span><span class="callnote text">Circulate loco. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
