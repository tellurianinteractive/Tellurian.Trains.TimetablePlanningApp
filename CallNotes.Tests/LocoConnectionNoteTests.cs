﻿using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoConnectionNoteTests
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
        var notes = await Notes(11);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Tu,Th,Sa,Su </span><span class="note-text">Connect loco </span><span class="note-value">SJ Rc6 turnus 2. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
        
        // These need only to be tested once
        Assert.IsFalse(note.IsForArrival);
        Assert.IsTrue(note.IsForDeparture);
        Assert.IsTrue(note.IsToLocoDriver);
        Assert.IsTrue(note.IsToDispatcher);
        Assert.IsTrue(note.IsToShunter);
    }

    [TestMethod]
    public async Task NoteWithOtherDays()
    {
        var notes = await Notes(12);
        var note = notes.First();

        const string expected =
            """
            <span class="note-days">Mo,We,Fr </span><span class="note-text">Connect loco </span><span class="note-value">SJ Rc6 turnus 1. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndCollectRemark()
    {
        var notes = await Notes(13);
        var note = notes.First();

        const string expected =
            """
            <span class="note-text">Connect loco </span><span class="note-value">SJ T44 232 turnus 2. </span><span class="note-text">Collect loco from staging area. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
