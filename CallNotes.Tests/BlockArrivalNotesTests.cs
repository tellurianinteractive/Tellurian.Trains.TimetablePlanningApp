using System.Linq;
using TimetablePlanning.Data;
using TimetablePlanning.Utilities.Extensions;
using TimetablePlanning.Models.CallNotes.Extensions;
using Microsoft.AspNetCore.Components;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class BlockArrivalNotesTests
{
    [TestMethod]
    public void SingleDestination()
    {
        var note = new BlockArrival()
        {
            CallId = 1,
            PositionInTrain = 2,
            DestinationFullName = "Göteborg",
            DestinationSignature = "Gbg",
            OriginFullName = "Uddevalla",
            OriginSignature = "Uv",
            TrainOperatingDaysFlags = OperationDays.Daily,
            DutyOperatingDaysFlags = OperationDays.Daily,
            Uncouple = true,
        }.AsEnumerable().AsBlockArrivalNote();

        const string expected = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), note.AsMarkup());
    }

    [TestMethod]
    public void MultipleDestination()
    {
        var notes =
             new BlockArrival[] {
                    new BlockArrival()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                        Uncouple = true,
                    },
                   new BlockArrival()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                        Uncouple = true,
                    },
             }.AsBlockArrivalNote();

        const string expected = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span><span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Göteborg</span>
            """;

        Assert.AreEqual(new MarkupString(expected), notes.AsMarkup());
    }

    [TestMethod]
    public void TwoCallDestination()
    {
        var notes =
             new BlockArrival[] {
                    new BlockArrival()
                    {
                        CallId = 2,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                        Uncouple = false,
                    },
                   new BlockArrival()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                        Uncouple = true,
                    },
             }.AsBlockArrivalNotes();

        const string expectedFirst = "";

        const string expectedLast = """
            <span class="note-text">Uncouple wagons to </span> <span class="note-destination" style="color: #000000;background-color: #C0C0C0;">Ytterby</span>
            """;

        Assert.AreEqual(new MarkupString(expectedFirst), notes.First().AsMarkup());
        Assert.AreEqual(new MarkupString(expectedLast), notes.Last().AsMarkup());

    }
}

