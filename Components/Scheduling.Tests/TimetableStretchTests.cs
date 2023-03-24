using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var target = Target;
            var (Start, End) = target.TimeLine(target.StartTime);
            Assert.AreEqual(target.Settings.TimeAxisSpacing.Y, Start.Y);
            Assert.AreEqual(target.Settings.KilometerAxisSpacing.X, Start.X);
            Assert.AreEqual(target.MaxTrackOffset.Y, End.Y);
            Assert.AreEqual(target.Settings.KilometerAxisSpacing.X, End.X);
        }

        [TestMethod]
        public void HorisontalTimeBeforeTimeScaleIsNull()
        {
            var target = Target;
            var time = target.Time(0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void HorisontalTimeIsStartHourAtStartOfTimeScale()
        {
            var target = Target;
            var time = target.Time(target.Settings.KilometerAxisSpacing.X, 0);
            Assert.AreEqual(target.StartTime, time);
        }

        [TestMethod]
        public void HorisontalTimeIsEndHourAtEndOfTimeScale()
        {
            var target = Target;
            var x = (int)((target.EndTime - target.StartTime).TotalMinutes * target.Settings.MinuteSpacing) + target.Settings.KilometerAxisSpacing.X;
            var time = target.Time(x, 0);
            Assert.AreEqual(target.EndTime, time);
        }

        [TestMethod]
        public void HorisontalTimeAfterTimeScaleIsNull()
        {
            var target = Target;
            var x = (int)((target.EndTime - target.StartTime).TotalMinutes * target.Settings.MinuteSpacing) + target.Settings.KilometerAxisSpacing.X + target.Settings.MinuteSpacing;
            var time = target.Time(x, 0);
            Assert.IsNull(time);
        }
        [TestMethod]
        public void VerticalTimeBeforeTimeScaleIsNull()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var time = target.Time(0, 0);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void VerticalTimeIsStartHourAtStartOfTimeScale()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var time = target.Time(0, target.Settings.KilometerAxisSpacing.Y);
            Assert.AreEqual(target.StartTime, time);
        }

        [TestMethod]
        public void VerticalTimeIsEndHourAtEndOfTimeScale()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var y = (int)((target.EndTime - target.StartTime).TotalMinutes * target.Settings.MinuteSpacing) + target.Settings.KilometerAxisSpacing.Y;
            var time = target.Time(0, y);
            Assert.AreEqual(target.EndTime, time);
        }

        [TestMethod]
        public void VerticalTimeAfterTimeScaleIsNull()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var y = (int)((target.EndTime - target.StartTime).TotalMinutes * target.Settings.MinuteSpacing) + target.Settings.KilometerAxisSpacing.Y + target.Settings.MinuteSpacing;
            var time = target.Time(0, y);
            Assert.IsNull(time);
        }

        [TestMethod]
        public void XOffsetBeforeTimeScaleIsInvalid()
        {
            var target = Target;
            var offset = target.TimeOffset(target.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void XOffsetAtStartOfTimeScale()
        {
            var target = Target;
            var offset = target.TimeOffset(target.StartTime);
            Assert.AreEqual(target.Settings.KilometerAxisSpacing.X, offset.X);
        }

        [TestMethod]
        public void XOffsetAtEndOfTimeScale()
        {
            var target = Target;
            var offset = target.TimeOffset(target.EndTime);
            var expected = target.Settings.KilometerAxisSpacing.X + (target.Settings.MinuteSpacing * (int)(target.EndTime - target.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.X);
        }

        [TestMethod]
        public void XOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var target = Target;
            var offset = target.TimeOffset(target.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetBeforeTimeScaleIsInvalid()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var offset = target.TimeOffset(target.StartTime.Add(-TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }

        [TestMethod]
        public void YOffsetAtStartOfTimeScale()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var offset = target.TimeOffset(target.StartTime);
            Assert.AreEqual(target.Settings.KilometerAxisSpacing.Y, offset.Y);
        }

        [TestMethod]
        public void YOffsetAtEndOfTimeScale()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var offset = target.TimeOffset(target.EndTime);
            var expected = target.Settings.KilometerAxisSpacing.Y + (target.Settings.MinuteSpacing * (int)(target.EndTime - target.StartTime).TotalMinutes);
            Assert.AreEqual(expected, offset.Y);
        }

        [TestMethod]
        public void YOffsetAfterEndOfTimeScaleIsInvalid()
        {
            var target = Target with { TimeAxisDirection = TimeAxisDirection.Vertical };
            var offset = target.TimeOffset(target.EndTime.Add(TimeSpan.FromMinutes(1)));
            Assert.IsTrue(offset.IsInvalid);
        }
    }
