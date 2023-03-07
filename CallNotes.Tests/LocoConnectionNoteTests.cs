using Microsoft.AspNetCore.Components;
using TimetablePlanning.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class LocoConnectionNoteTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
    }

    [TestMethod]
    public void NoteWithDays()
    {
        var note = new LocoConnect(){ 
            CallId = 24, 
            LocoOperatorSignature = "SJ", 
            LocoClass = "Rc6", 
            LocoNumber = "",
            TurnNumber = "2", 
            LocoOperatingDaysFlags = 0b01101010, 
            TrainOperatingDaysFlags = OperationDays.Daily, 
            DutyOperatingDaysFlags = OperationDays.Daily 
        }.AsLocoConnectioNote();

        const string expected =
            """
            <span class="note-days">Tu,Th,Sa,Su </span><span class="note-text">Connect loco </span><span class="note-item">SJ Rc6 turn 2. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithOtherDays()
    {
        var note = new LocoConnect()
        {
            CallId = 24,
            LocoOperatorSignature = "SJ",
            LocoClass = "Rc6",
            LocoNumber = "",
            TurnNumber = "1",
            LocoOperatingDaysFlags = 0b00010101,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily
        }.AsLocoConnectioNote();

        const string expected =
            """
            <span class="note-days">Mo,We,Fr </span><span class="note-text">Connect loco </span><span class="note-item">SJ Rc6 turn 1. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void NoteWithLocoNumberAndCollectRemark()
    {
        var note = new LocoConnect()
        {
            CallId = 24,
            LocoOperatorSignature = "SJ",
            LocoClass = "T44",
            LocoNumber = "232",
            TurnNumber = "3",
            LocoOperatingDaysFlags = 0b00010101,
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = 0b00010101,
            CollectFromStagingArea = true,
        }.AsLocoConnectioNote();

        const string expected =
            """
            <span class="note-text">Connect loco </span><span class="note-item">SJ T44 232 turn 3. </span><span class="note-text">Collect loco from staging area. </span>
            """;
        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }
}
