using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoDisconnectionNoteTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
    }

    [TestMethod]
    public void NoteWithDays()
    {
        var note = new LocoDisconnectEvent()
        {
            CallId = 24,
            LocoOperatorSignature = "SJ",
            LocoClass = "Rc6",
            TurnNumber = "2",
            LocoOperatingDaysFlags = 0b01101010,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily
        }.AsLocoDisconnectNote();

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
    public void NoteWithOtherDays()
    {
        var note = new LocoDisconnectEvent()
        {
            CallId = 24,
            LocoOperatorSignature = "SJ",
            LocoClass = "Rc6",
            LocoNumber = "",
            TurnNumber = "1",
            LocoOperatingDaysFlags = 0b00010101,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-days">Mo,We,Fr </span><span class="note-text">Disconnect loco </span><span class="note-item">SJ Rc6 turn 1. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndDriveStoStagingAreaRemark()
    {
        var note = new LocoDisconnectEvent()
        {
            CallId = 25,
            LocoOperatorSignature = "SJ",
            LocoClass = "T44",
            LocoNumber = "232",
            TurnNumber = "3",
            LocoOperatingDaysFlags = 0b00010101,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = 0b00010101,
            DriveToStagingArea = true
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 232 turn 3. </span><span class="note-text">Drive loco to staging area. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndTurnRemark()
    {
        var note = new LocoDisconnectEvent()
        {
            LocoOperatorSignature = "SJ",
            CallId = 25,
            LocoClass = "T44",
            LocoNumber = "236",
            TurnNumber = "8",
            LocoOperatingDaysFlags = OperationDays.Daily,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            TurnLoco = true
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Turn loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndCirculateRemark()
    {
        var note = new LocoDisconnectEvent()
        {
            LocoOperatorSignature = "SJ",
            CallId = 25,
            LocoClass = "T44",
            LocoNumber = "236",
            TurnNumber = "8",
            LocoOperatingDaysFlags = OperationDays.Daily,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            CirculateLoco = true
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndBothTurnAndCirculateRemark()
    {
        var note = new LocoDisconnectEvent()
        {
            LocoOperatorSignature = "SJ",
            CallId = 25,
            LocoClass = "T44",
            LocoNumber = "236",
            TurnNumber = "8",
            LocoOperatingDaysFlags = OperationDays.Daily,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            CirculateLoco = true,
            TurnLoco= true,
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Turn loco. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndBothDriveToStagingAreaAndTurnLocoRemarks()
    {
        var note = new LocoDisconnectEvent()
        {
            LocoOperatorSignature = "SJ",
            CallId = 25,
            LocoClass = "T44",
            LocoNumber = "236",
            TurnNumber = "8",
            LocoOperatingDaysFlags = OperationDays.Daily,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            DriveToStagingArea = true,
            TurnLoco = true,
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Drive loco to staging area. </span><span class="note-text">Turn loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithDriveToStagingAreaAndTurnLocoAndCirculateIsError()
    {
        var note = new LocoDisconnectEvent()
        {
            LocoOperatorSignature = "SJ",
            CallId = 25,
            LocoClass = "T44",
            LocoNumber = "236",
            TurnNumber = "8",
            LocoOperatingDaysFlags = OperationDays.Daily,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            DriveToStagingArea = true,
            TurnLoco = true,
            CirculateLoco = true,
        }.AsLocoDisconnectNote();

        const string expected =
            """
            <span class="note-text">Disconnect loco </span><span class="note-item">SJ T44 236 turn 8. </span><span class="note-text">Drive loco to staging area. </span><span class="note-text">Turn loco. </span><span class="note-text">Circulate loco. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
