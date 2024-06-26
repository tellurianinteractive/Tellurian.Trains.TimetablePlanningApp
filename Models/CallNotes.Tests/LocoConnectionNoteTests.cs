﻿using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoConnectionNoteTests : NoteTestsBase
{
    [TestMethod]
    public async Task NoteWithDays()
    {
        var notes = await Notes(11);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote days">Tu,Th,Sa,Su: </span><span class="callnote text">Connect loco </span><span class="callnote value">SJ Rc6 turnus 2. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
        
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

            <div class="callnote"><span class="callnote days">Mo,We,Fr: </span><span class="callnote text">Connect loco </span><span class="callnote value">SJ Rc6 turnus 1. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task NoteWithLocoNumberAndCollectRemark()
    {
        var notes = await Notes(13);
        var note = notes.First();

        const string expected =
            """

            <div class="callnote"><span class="callnote text">Connect loco </span><span class="callnote value">SJ T44 232 turnus 2. </span><span class="callnote text">Pick up locomotives from the staging area. </span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }
}
