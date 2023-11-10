using TimetablePlanning.Models.Common;

namespace Models.Common.Tests;

[TestClass]
public class OperationDaysTests
{
    [TestMethod]
    public void MoWeFrAllDaysOfDaily()
    {
        Assert.IsTrue(OperationDays.Daily.IsAllDaysOf(OperationDayFlags.MoWeFr.ToOperationDays()));
    }

    [TestMethod]
    public void DailyIsNotAllDaysOfMoWeFr()
    {
        Assert.IsFalse(OperationDayFlags.MoWeFr.ToOperationDays().IsAllDaysOf(OperationDays.Daily));

    }
}