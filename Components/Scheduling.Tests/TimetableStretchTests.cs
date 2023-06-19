using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimetablePlanning.Components.Scheduling;
using TimetablePlanning.Components.Scheduling.Extensions;

namespace TimetablePlanning.Scheduling.Tests;

[TestClass]
    public class TimetableStretchTests
    {
        private static TimetableStretch Target =>
            TimetableBuilder.Bohusbanan;

        [TestMethod]
        public void TimeLineOffsetsAreCorrect()
        {
            var (Start, End) = Target.TimeLine(TimeAxisDirection.Horisontal, Target.StartTime);
            Assert.AreEqual(Target.Settings.TimeAxisSpacing.Y, Start.Y);
            Assert.AreEqual(Target.Settings.KilometerAxisSpacing.X, Start.X);
            Assert.AreEqual(Target.MaxTrackOffset(TimeAxisDirection.Horisontal).Y, End.Y);
            Assert.AreEqual(Target.Settings.KilometerAxisSpacing.X, End.X);
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
            var time = Target.Time(TimeAxisDirection.Horisontal, Target.Settings.KilometerAxisSpacing.X, 0);
            Assert.AreEqual(Target.StartTime, time);
        }

        [TestMethod]
        public void HorisontalTimeIsEndHourAtEndOfTimeScale()
        {
            var x = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.Settings.MinuteSpacing) + Target.Settings.KilometerAxisSpacing.X;
            var time = Target.Time(TimeAxisDirection.Horisontal, x, 0);
            Assert.AreEqual(Target.EndTime, time);
        }

        [TestMethod]
        public void HorisontalTimeAfterTimeScaleIsNull()
        {
            var x = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.Settings.MinuteSpacing) + Target.Settings.KilometerAxisSpacing.X + Target.Settings.MinuteSpacing;
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
            var time = Target.Time(TimeAxisDirection.Vertical, 0, Target.Settings.KilometerAxisSpacing.Y);
            Assert.AreEqual(Target.StartTime, time);
        }

        [TestMethod]
        public void VerticalTimeIsEndHourAtEndOfTimeScale()
        {
            var y = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.Settings.MinuteSpacing) + Target.Settings.KilometerAxisSpacing.Y;
            var time = Target.Time(TimeAxisDirection.Vertical, 0, y);
            Assert.AreEqual(Target.EndTime, time);
        }

        [TestMethod]
        public void VerticalTimeAfterTimeScaleIsNull()
        {
            var y = (int)((Target.EndTime - Target.StartTime).TotalMinutes * Target.Settings.MinuteSpacing) + Target.Settings.KilometerAxisSpacing.Y + Target.Settings.MinuteSpacing;
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
            Assert.AreEqual(Target.Settings.KilometerAxisSpacing.X, offset.X);
        }

        [TestMethod]
        public void XOffsetAtEndOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Horisontal, Target.EndTime);
            var expected = Target.Settings.KilometerAxisSpacing.X + (Target.Settings.MinuteSpacing * (int)(Target.EndTime - Target.StartTime).TotalMinutes);
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
            Assert.AreEqual(Target.Settings.KilometerAxisSpacing.Y, offset.Y);
        }

        [TestMethod]
        public void YOffsetAtEndOfTimeScale()
        {
            var offset = Target.TimeOffset(TimeAxisDirection.Vertical, Target.EndTime);
            var expected = Target.Settings.KilometerAxisSpacing.Y + (Target.Settings.MinuteSpacing * (int)(Target.EndTime - Target.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.Y);
        }

        [TestMethod]
        public void YOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var offset = Target.TimeOffset( TimeAxisDirection.Vertical,Target.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }
    }
