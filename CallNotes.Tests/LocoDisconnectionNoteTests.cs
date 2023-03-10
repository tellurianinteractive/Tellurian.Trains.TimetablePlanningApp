using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoDisconnectionNoteTests
{
    private ICallEventsService? CallEventsService;


    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallEventsService = new TestCallEventsService();
    }

    private async Task<IEnumerable<LocoDisconnectNote>> Notes(int testCase)
    {
        var events = await CallEventsService!.GetLocoDisconnectEventsAsync(testCase).ConfigureAwait(false);
        return events.AsLocoDisconnectNotes();
    }

    [TestMethod]
    public async Task NoteWithDays()
    {
        var notes = await Notes(1);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Tu,Th,Sa,Su </span><span class="note-text">Disconnect loco </span><span class="note-item">SJ Rc6 turn 2. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());

        // These need only to be tested once
        Assert.IsTrue(note.IsForArrival);
        Assert.IsFalse(note.IsForDeparture);
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
            <span class="note-days">Mo,We,Fr </span><span class="note-text">Disconnect loco </span><span class="note-item">SJ Rc6 turn 1. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndDriveStoStagingAreaRemark()
    {
        var notes = await Notes(3);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 232 turn 3. </span><span class="note-text">Drive loco to staging area. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndTurnRemark()
    {
        var notes = await Notes(4);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Turn loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndCirculateRemark()
    {
        var notes = await Notes(5);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndBothTurnAndCirculateRemark()
    {
        var notes = await Notes(6);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Turn loco. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndBothDriveToStagingAreaAndTurnLocoRemarks()
    {
        var notes = await Notes(7);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Drive loco to staging area. </span><span class="note-text">Turn loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithDriveToStagingAreaAndTurnLocoAndCirculateIsError()
    {
        var notes = await Notes(8);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Drive loco to staging area. </span><span class="note-text">Turn loco. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
