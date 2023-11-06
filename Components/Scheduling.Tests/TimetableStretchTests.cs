using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimetablePlanning.Components.Scheduling;
using TimetablePlanning.Components.Scheduling.Extensions;

namespace TimetablePlanning.Scheduling.Tests;

[TestClass]
    public class TimetableStretchTests
    {
        private static Schedule TargetHorisontal =>
            ScheduleBuilder.Bohusbanan();
        private static Schedule TargetVertical => ScheduleBuilder.Bohusbanan(GraphSettings.Default with { AxisDirection=TimeAxisDirection.Vertical});

        [TestMethod]
        public void TimeLineOffsetsAreCorrect()
        {
            var (Start, End) = TargetHorisontal.TimeLine(TargetHorisontal.StartTime);
            Assert.AreEqual(TargetHorisontal.GraphSettings.TimeAxisSpacing.Y, Start.Y);
            Assert.AreEqual(TargetHorisontal.GraphSettings.KilometerAxisSpacing.X, Start.X);
            Assert.AreEqual(TargetHorisontal.MaxTrackOffset().Y, End.Y);
            Assert.AreEqual(TargetHorisontal.GraphSettings.KilometerAxisSpacing.X, End.X);
        }

        [TestMethod]
        public void HorisontalTimeBeforeTimeScaleIsNull()
        {
            var time = TargetHorisontal.Time( 0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void HorisontalTimeIsStartHourAtStartOfTimeScale()
        {
            var time = TargetHorisontal.Time( TargetHorisontal.GraphSettings.KilometerAxisSpacing.X, 0);
            Assert.AreEqual(TargetHorisontal.StartTime, time);
        }

        [TestMethod]
        public void HorisontalTimeIsEndHourAtEndOfTimeScale()
        {
            var x = (int)((TargetHorisontal.EndTime - TargetHorisontal.StartTime).TotalMinutes * TargetHorisontal.GraphSettings.MinuteSpacing) + TargetHorisontal.GraphSettings.KilometerAxisSpacing.X;
            var time = TargetHorisontal.Time(x, 0);
            Assert.AreEqual(TargetHorisontal.EndTime, time);
        }

        [TestMethod]
        public void HorisontalTimeAfterTimeScaleIsNull()
        {
            var x = (int)((TargetHorisontal.EndTime - TargetHorisontal.StartTime).TotalMinutes * TargetHorisontal.GraphSettings.MinuteSpacing) + TargetHorisontal.GraphSettings.KilometerAxisSpacing.X + TargetHorisontal.GraphSettings.MinuteSpacing;
            var time = TargetHorisontal.Time(x    , 0);
            Assert.IsNull(time);
        }
        [TestMethod]
        public void VerticalTimeBeforeTimeScaleIsNull()
        {
            var time = TargetHorisontal.Time(0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void VerticalTimeIsStartHourAtStartOfTimeScale()
        {
            var time = TargetVertical.Time(0, TargetVertical.GraphSettings.KilometerAxisSpacing.Y);
            Assert.AreEqual(TargetVertical.StartTime, time);
        }

        [TestMethod]
        public void VerticalTimeIsEndHourAtEndOfTimeScale()
        {
            var y = (int)((TargetVertical.EndTime - TargetVertical.StartTime).TotalMinutes * TargetVertical.GraphSettings.MinuteSpacing) + TargetVertical.GraphSettings.KilometerAxisSpacing.Y;
            var time = TargetVertical.Time(0, y);
            Assert.AreEqual(TargetVertical.EndTime, time);
        }

        [TestMethod]
        public void VerticalTimeAfterTimeScaleIsNull()
        {
            var y = (int)((TargetHorisontal.EndTime - TargetHorisontal.StartTime).TotalMinutes * TargetHorisontal.GraphSettings.MinuteSpacing) + TargetHorisontal.GraphSettings.KilometerAxisSpacing.Y + TargetHorisontal.GraphSettings.MinuteSpacing;
            var time = TargetHorisontal.Time(0, y);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void XOffsetBeforeTimeScaleIsInvalid()
        {
            var offset = TargetHorisontal.TimeOffset(      TargetHorisontal.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void XOffsetAtStartOfTimeScale()
        {
            var offset = TargetHorisontal.TimeOffset(TargetHorisontal.StartTime);
            Assert.AreEqual(TargetHorisontal.GraphSettings.KilometerAxisSpacing.X, offset.X);
        }

        [TestMethod]
        public void XOffsetAtEndOfTimeScale()
        {
            var offset = TargetHorisontal.TimeOffset(TargetHorisontal.EndTime);
            var expected = TargetHorisontal.GraphSettings.KilometerAxisSpacing.X + (TargetHorisontal.GraphSettings.MinuteSpacing * (int)(TargetHorisontal.EndTime - TargetHorisontal.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.X);
        }

        [TestMethod]
        public void XOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var offset = TargetHorisontal.TimeOffset( TargetHorisontal.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetBeforeTimeScaleIsInvalid()
        {
            var offset = TargetHorisontal.TimeOffset(TargetHorisontal.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetAtStartOfTimeScale()
        {
            var offset = TargetVertical.TimeOffset(TargetVertical.StartTime);
            Assert.AreEqual(TargetVertical.GraphSettings.KilometerAxisSpacing.Y, offset.Y);
        }

        [TestMethod]
        public void YOffsetAtEndOfTimeScale()
        {
            var offset = TargetVertical.TimeOffset(TargetVertical.EndTime);
            var expected = TargetVertical.GraphSettings.KilometerAxisSpacing.Y + (TargetVertical.GraphSettings.MinuteSpacing * (int)(TargetVertical.EndTime - TargetHorisontal.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.Y);
        }

        [TestMethod]
        public void YOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var offset = TargetHorisontal.TimeOffset( TargetHorisontal.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }
    }
