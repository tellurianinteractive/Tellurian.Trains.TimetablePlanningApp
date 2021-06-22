using System;
using System.Runtime.CompilerServices;
using Tellurian.Trains.Scheduling.Model;

[assembly: InternalsVisibleTo("Tellurian.Trains.Scheduling.Tests")]
namespace Tellurian.Trains.Scheduling.Extensions
{
    public static class TimetableStretchExtensions
    {
        public static string OrientationCss(this TimetableStretch me, string classes) =>
            me.HasHorizontalTimeAxis ? $"{classes} horizontal".TrimStart() :
            me.HasVerticalTimeAxis ? $"{classes} vertical".TrimStart() :
            string.Empty;

        public static (Offset Start, Offset End) TrackLine(this TimetableStretch me, int stationIndex, int trackIndex)
        {
            if (me.HasHorizontalTimeAxis)
            {
                var y = me.Y(stationIndex, trackIndex);
                return (new(me.Settings.KilometerAxisSpacing.X, y), new(me.MaxTimeOffset.X, y));
            }
            else if (me.HasVerticalTimeAxis)
            {
                var x = me.X(stationIndex, trackIndex);
                return (new(x, me.Settings.KilometerAxisSpacing.Y), new(x, me.MaxTimeOffset.Y));
            }
            throw new NotSupportedException();
        }

        public static (Offset Start, Offset End) TimeLine(this TimetableStretch me, TimeSpan time)
        {
            if (me.HasHorizontalTimeAxis)
            {
                var x = me.TimeOffset(time).X;
                return (new(x, me.Settings.TimeAxisSpacing.Y), new(x, me.MaxTrackOffset.Y));
            }
            else if (me.HasVerticalTimeAxis)
            {
                var y = me.TimeOffset(time).Y;
                return (new(me.Settings.TimeAxisSpacing.X, y), new(me.MaxTrackOffset.X, y));
            }
            throw new NotSupportedException();
        }

        public static Offset TimeAxisLabelOffset(this TimetableStretch me, TimeSpan time) =>
            me.TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => me.TimeLine(time).Start - new Offset(5, 5),
                TimeAxisDirection.Vertical => me.TimeLine(time).Start - new Offset(me.Settings.TimeAxisSpacing.X-5, 0),
                _ => throw new NotSupportedException()
            };

        public static Offset StationLabelOffset(this TimetableStretch me, int stationIndex)
        {
            var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.Settings.TrackSpacing;
            return me.TimeAxisDirection switch
                {
                    TimeAxisDirection.Horisontal => new(5, me.Y(stationIndex, 0) + offset),
                    TimeAxisDirection.Vertical => new(me.X(stationIndex, 0) + offset, 25),
                    _ => Offset.Invalid
                };
        }

        public static Offset KmLabelOffset(this TimetableStretch me, int stationIndex)
        {
            var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.Settings.TrackSpacing;
            return me.TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => new(me.Settings.KilometerAxisSpacing.X - 5, me.Y(stationIndex, 0) + offset),
                TimeAxisDirection.Vertical => new(me.X(stationIndex, 0) + offset, me.Settings.KilometerAxisSpacing.Y - 5),
                _ => Offset.Invalid
            };
        }

        public static TimeSpan? Time(this TimetableStretch me, int xOffset, int yOffset)
        {
            if (me.HasHorizontalTimeAxis)
            {
                var x = xOffset - me.Settings.KilometerAxisSpacing.X;
                var time = TimeSpan.FromMinutes(x / me.Settings.MinuteSpacing).Add(me.StartTime);
                return time >= me.StartTime && time <= me.EndTime ? time : null;
            }
            else if (me.HasVerticalTimeAxis)
            {
                var y = yOffset - me.Settings.KilometerAxisSpacing.Y;
                var time = TimeSpan.FromMinutes(y / me.Settings.MinuteSpacing).Add(me.StartTime);
                return time >= me.StartTime && time <= me.EndTime ? time : null;
            }
            throw new NotSupportedException(me.TimeAxisDirection.ToString());
        }

        public static Offset TimeOffset(this TimetableStretch me, TimeSpan time)
        {
            if (time < me.StartTime || time > me.EndTime) return Offset.Invalid;
            if (me.HasHorizontalTimeAxis)
            {
                var x = me.Settings.KilometerAxisSpacing.X + (me.Settings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
                return new(x, 0);
            }
            else if (me.HasVerticalTimeAxis)
            {
                var y = me.Settings.KilometerAxisSpacing.Y + (me.Settings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
                return new(0, y);
            }
            throw new NotSupportedException(me.TimeAxisDirection.ToString());
        }

        public static int X(this TimetableStretch me, int stationIndex, int trackIndex) =>
            me.TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => TrackOffset(me, stationIndex, trackIndex).Y,
                TimeAxisDirection.Vertical => TrackOffset(me, stationIndex, trackIndex).X,
                _ => 0
            };

        public static int Y(this TimetableStretch me, int stationIndex, int trackIndex) =>
             me.TimeAxisDirection switch
             {
                 TimeAxisDirection.Horisontal => TrackOffset(me, stationIndex, trackIndex).Y,
                 TimeAxisDirection.Vertical => TrackOffset(me, stationIndex, trackIndex).X,
                 _ => 0
             };

        public static Offset TrackOffset(this TimetableStretch me, int stationIndex, int trackIndex)
        {
            var x = me.Settings.TimeAxisSpacing.X;
            var y = me.Settings.TimeAxisSpacing.Y;
            if (stationIndex == 0)
            {
                y += me.Settings.TrackSpacing * trackIndex;
                x += me.Settings.TrackSpacing * trackIndex;
            }
            else
            {
                for (var i = 0; i < stationIndex; i++)
                {
                    var stretch = me.TrackStreches[i];
                    var Δ1 = Math.Max(me.Settings.MinStationSpacing, ((stretch.From.Tracks.Length - 1) * me.Settings.TrackSpacing) + (me.Settings.KilometerSpacing * stretch.Km));
                    x += Δ1;
                    y += Δ1;
                }
                var Δ2 = trackIndex * me.Settings.TrackSpacing;
                x += Δ2;
                y += Δ2;
            }
            return me.TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => new(0, y),
                TimeAxisDirection.Vertical => new(x, 0),
                _ => Offset.Invalid
            };
        }

        public static Offset MaxTrackOffset(this TimetableStretch me)
        {
            var x = me.Settings.TimeAxisSpacing.X + ((me.TrackStreches[0].From.Tracks.Length - 1) * me.Settings.TrackSpacing);
            var y = me.Settings.TimeAxisSpacing.Y + ((me.TrackStreches[0].From.Tracks.Length - 1) * me.Settings.TrackSpacing);
            for (var i = 0; i < me.TrackStreches.Length; i++)
            {
                var stretch = me.TrackStreches[i];
                var Δ = Math.Max(me.Settings.MinStationSpacing, (me.Settings.KilometerSpacing * stretch.Km) + ((stretch.To.Tracks.Length - 1) * me.Settings.TrackSpacing));
                x += Δ;
                y += Δ;
            }

            return me.TimeAxisDirection switch
            {
                TimeAxisDirection.Horisontal => new(0, y),
                TimeAxisDirection.Vertical => new(x, 0),
                _ => Offset.Invalid
            };
        }
    }
}
