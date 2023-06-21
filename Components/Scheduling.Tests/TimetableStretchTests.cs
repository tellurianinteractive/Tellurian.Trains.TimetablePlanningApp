using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimetablePlanning.Components.Scheduling;
using TimetablePlanning.Components.Scheduling.Extensions;

namespace TimetablePlanning.Scheduling.Tests;

[TestClass]
    public class TimetableStretchTests
    {
        private static Schedule Target =>
            ScheduleBuilder.Bohusbanan;

        [TestMethod]
        public void TimeLineOffsetsAreCorrect()
        {
            var (Start, End) = Target.TimeLine(TimeAxisDirection.Horisontal, Target.StartTime);
            Assert.AreEqual(Target.GraphSettings.TimeAxisSpacing.Y, Start.Y);
            Assert.AreEqual(Target.GraphSettings.KilometerAxisSpacing.X, Start.X);
            Assert.AreEqual(Target.MaxTrackOffset(TimeAxisDirection.Horisontal).Y, End.Y);
            Assert.AreEqual(Target.GraphSettings.KilometerAxisSpacing.X, End.X);
        }

        [TestMethod]
        public void HorisontalTimeBeforeTimeScaleIsNull()
        {
            var time = Target.Time(TimeAxisDirection.Horisontal, 0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void HorisontalTimeIsStartHourAtStartOfTimeScale()
        {
            var time = Target.Time(TimeAxisDirection.Horisontal, Target.GraphSettings.KilometerAxisSpacing.X, 0);
            Assert.AreEqual(Target.StartTime, time);
        }

        [TestMethod]
        public void HorisontalTimeIsEndHourAtEndOfTimeScale()
        {
            var x = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.GraphSettings.MinuteSpacing) + Target.GraphSettings.KilometerAxisSpacing.X;
            var time = Target.Time(TimeAxisDirection.Horisontal, x, 0);
            Assert.AreEqual(Target.EndTime, time);
        }

        [TestMethod]
        public void HorisontalTimeAfterTimeScaleIsNull()
        {
            var x = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.GraphSettings.MinuteSpacing) + Target.GraphSettings.KilometerAxisSpacing.X + Target.GraphSettings.MinuteSpacing;
            var time = Target.Time(TimeAxisDirection.Horisontal, x    , 0);
            Assert.IsNull(time);
        }
        [TestMethod]
        public void VerticalTimeBeforeTimeScaleIsNull()
        {
            var time = Target.Time(TimeAxisDirection.Vertical, 0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void VerticalTimeIsStartHourAtStartOfTimeScale()
        {
            var time = Target.Time(TimeAxisDirection.Vertical, 0, Target.GraphSettings.KilometerAxisSpacing.Y);
            Assert.AreEqual(Target.StartTime, time);
        }

        [TestMethod]
        public void VerticalTimeIsEndHourAtEndOfTimeScale()
        {
            var y = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.GraphSettings.MinuteSpacing) + Target.GraphSettings.KilometerAxisSpacing.Y;
            var time = Target.Time(TimeAxisDirection.Vertical, 0, y);
            Assert.AreEqual(Target.EndTime, time);
        }

        [TestMethod]
        public void VerticalTimeAfterTimeScaleIsNull()
        {
            var y = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.GraphSettings.MinuteSpacing) + Target.GraphSettings.KilometerAxisSpacing.Y + Target.GraphSettings.MinuteSpacing;
            var time = Target.Time(TimeAxisDirection.Vertical ,0, y);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void XOffsetBeforeTimeScaleIsInvalid()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Horisontal, Target.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void XOffsetAtStartOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Horisontal, Target.StartTime);
            Assert.AreEqual(Target.GraphSettings.KilometerAxisSpacing.X, offset.X);
        }

        [TestMethod]
        public void XOffsetAtEndOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Horisontal, Target.EndTime);
            var expected = Target.GraphSettings.KilometerAxisSpacing.X + (Target.GraphSettings.MinuteSpacing * (int)(Target.EndTime - Target.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.X);
        }

        [TestMethod]
        public void XOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var offset = Target.TimeOffset( TimeAxisDirection.Horisontal, Target.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetBeforeTimeScaleIsInvalid()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Vertical, Target.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetAtStartOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Vertical, Target.StartTime);
            Assert.AreEqual(Target.GraphSettings.KilometerAxisSpacing.Y, offset.Y);
        }

        [TestMethod]
        public void YOffsetAtEndOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Vertical, Target.EndTime);
            var expected = Target.GraphSettings.KilometerAxisSpacing.Y + (Target.GraphSettings.MinuteSpacing * (int)(Target.EndTime - Target.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.Y);
        }

        [TestMethod]
        public void YOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var offset = Target.TimeOffset( TimeAxisDirection.Vertical,Target.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }
    }
