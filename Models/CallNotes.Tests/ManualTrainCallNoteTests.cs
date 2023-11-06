using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class ManualTrainCallNoteTests
{
    [TestMethod]
    public void WhenNoTextAddedReturnsEnptyString()
    {
        var target = new ManualTrainCallNote()
        {
            DutyOperationDays = OperationDays.Daily,
            TrainOperationDays = OperationDays.Daily,
            IsForArrival = true,
            IsToLocoDriver = true,
        };

        Assert.AreEqual(string.Empty, target.Markup().ToString());
    }
}
