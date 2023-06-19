using System.Runtime.CompilerServices;
using TimetablePlanning.Models.Common;

[assembly: InternalsVisibleTo("TimetablePlanning.Scheduling.Tests")]
namespace TimetablePlanning.Components.Scheduling.Extensions;

public static class TimetableStretchExtensions
{
    public static string OrientationCss(this TimeAxisDirection axisDirection, string classes) =>
        axisDirection == TimeAxisDirection.Horisontal ? $"{classes} horizontal".TrimStart() :
        axisDirection == TimeAxisDirection.Vertical? $"{classes} vertical".TrimStart() :
        string.Empty;


    public static (Offset Start, Offset End) TrackLine(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex)
    {
        if (axisDirection == TimeAxisDirection.Horisontal)
        {
            var y = me.Y(axisDirection, stationIndex, trackIndex);
            return (new(me.Settings.KilometerAxisSpacing.X, y), new(me.MaxTimeOffset(axisDirection).X, y));
        }
        else if (axisDirection == TimeAxisDirection.Vertical)
        {
            var x = me.X(axisDirection, stationIndex, trackIndex);
            return (new(x, me.Settings.KilometerAxisSpacing.Y), new(x, me.MaxTimeOffset(axisDirection).Y));
        }
        throw new NotSupportedException(axisDirection.ToString());
    }

    public static (Offset Start, Offset End) TimeLine(this TimetableStretch me, TimeAxisDirection axisDirection, TimeSpan time)
    {
        if (axisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.TimeOffset(axisDirection, time).X;
            return (new(x, me.Settings.TimeAxisSpacing.Y), new(x, me.MaxTrackOffset(axisDirection).Y));
        }
        else if (axisDirection == TimeAxisDirection.Vertical)
        {
            var y = me.TimeOffset(axisDirection, time).Y;
            return (new(me.Settings.TimeAxisSpacing.X, y), new(me.MaxTrackOffset(axisDirection).X, y));
        }
        throw new NotSupportedException(axisDirection.ToString());
    }

    public static int Height(this TimetableStretch me, TimeAxisDirection axisDirection) =>
        axisDirection switch
        {
            TimeAxisDirection.Horisontal => me.MaxTrackOffset(axisDirection).Y + me.Settings.EndMargin,
            TimeAxisDirection.Vertical => me.MaxTimeOffset(axisDirection).Y + me.Settings.EndMargin,
            _ => 0
        };

    public static int Width(this TimetableStretch me, TimeAxisDirection axisDirection) =>
        axisDirection switch
        {
            TimeAxisDirection.Horisontal => me.MaxTimeOffset(axisDirection).X + me.Settings.EndMargin,
            TimeAxisDirection.Vertical => me.MaxTrackOffset(axisDirection).X + me.Settings.EndMargin,
            _ => 0
        };

    public static Offset TimeAxisLabelOffset(this TimetableStretch me, TimeAxisDirection axisDirection, TimeSpan time) =>
       axisDirection switch
       {
           TimeAxisDirection.Horisontal => me.TimeLine(axisDirection, time).Start - new Offset(5, 5),
           TimeAxisDirection.Vertical => me.TimeLine(axisDirection, time).Start - new Offset(me.Settings.TimeAxisSpacing.X - 5, 0),
           _ => throw new NotSupportedException()
       };

    public static Offset StationLabelOffset(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex)
    {
        var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.Settings.TrackSpacing;
        return axisDirection switch
        {
            TimeAxisDirection.Horisontal => new(5, me.Y(axisDirection, stationIndex, 0) + offset),
            TimeAxisDirection.Vertical => new(me.X(axisDirection, stationIndex, 0) + offset, 25),
            _ => Offset.Invalid
        };
    }

    public static Offset KmLabelOffset(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex)
    {
        var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.Settings.TrackSpacing;
        return axisDirection switch
        {
            TimeAxisDirection.Horisontal => new(me.Settings.KilometerAxisSpacing.X - 15, me.Y(axisDirection, stationIndex, 0) + offset),
            TimeAxisDirection.Vertical => new(me.X(axisDirection, stationIndex, 0) + offset, me.Settings.KilometerAxisSpacing.Y - 15),
            _ => Offset.Invalid
        };
    }

    public static Offset TrackNumberOffset(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex)
    {
        return axisDirection switch
        {
            TimeAxisDirection.Horisontal => new(me.Settings.KilometerAxisSpacing.X - 2, me.Y(axisDirection, stationIndex, trackIndex) + 3),
            TimeAxisDirection.Vertical => new(me.X(axisDirection, stationIndex, trackIndex) + 0, me.Settings.KilometerAxisSpacing.Y - 2),
            _ => Offset.Invalid
        };

    }

    public static TimeSpan? Time(this TimetableStretch me, TimeAxisDirection axisDirection, int xOffset, int yOffset)
    {
        if (axisDirection == TimeAxisDirection.Horisontal)
        {
            var x = xOffset - me.Settings.KilometerAxisSpacing.X;
            var time = TimeSpan.FromMinutes(x / me.Settings.MinuteSpacing).Add(me.StartTime);
            return time >= me.StartTime && time <= me.EndTime ? time : null;
        }
        else if (axisDirection == TimeAxisDirection.Vertical)
        {
            var y = yOffset - me.Settings.KilometerAxisSpacing.Y;
            var time = TimeSpan.FromMinutes(y / me.Settings.MinuteSpacing).Add(me.StartTime);
            return time >= me.StartTime && time <= me.EndTime ? time : null;
        }
        throw new NotSupportedException(axisDirection.ToString());
    }


    public static Offset TimeOffset(this TimetableStretch me, TimeAxisDirection axisDirection, TimeSpan time)
    {
        if (time < me.StartTime || time > me.EndTime) return Offset.Invalid;
        if (axisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.Settings.KilometerAxisSpacing.X + (me.Settings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
            return new(x, 0);
        }
        else if (axisDirection == TimeAxisDirection.Vertical)
        {
            var y = me.Settings.KilometerAxisSpacing.Y + (me.Settings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
            return new(0, y);
        }
        throw new NotSupportedException(axisDirection.ToString());
    }

    public static Offset MaxTimeOffset(this TimetableStretch me, TimeAxisDirection axisDirection) =>
        TimeOffset(me, axisDirection, me.EndTime);

    public static int X(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex) =>
        axisDirection switch
        {
            TimeAxisDirection.Horisontal => TrackOffset(me, axisDirection, stationIndex, trackIndex).Y,
            TimeAxisDirection.Vertical => TrackOffset(me, axisDirection, stationIndex, trackIndex).X,
            _ => 0
        };

    public static int Y(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex) =>
         axisDirection switch
         {
             TimeAxisDirection.Horisontal => TrackOffset(me, axisDirection, stationIndex, trackIndex).Y,
             TimeAxisDirection.Vertical => TrackOffset(me, axisDirection, stationIndex, trackIndex).X,
             _ => 0
         };

    public static Offset TrackStartLocation(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex) =>
          me.TrackOffset(axisDirection, stationIndex, trackIndex) + me.TimeOffset(axisDirection, me.StartTime);

    public static Offset TrackEndLocation(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex) =>
         me.TrackOffset(axisDirection, stationIndex, trackIndex) + me.TimeOffset(axisDirection, me.EndTime);

    public static Offset TrackOffset(this TimetableStretch me, TimeAxisDirection axisDirection, int stationIndex, int trackIndex)
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
        return axisDirection switch
        {
            TimeAxisDirection.Horisontal => new(0, y),
            TimeAxisDirection.Vertical => new(x, 0),
            _ => Offset.Invalid
        };
    }

    public static Offset MaxTrackOffset(this TimetableStretch me, TimeAxisDirection axisDirection)
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

        return axisDirection switch
        {
            TimeAxisDirection.Horisontal => new(0, y),
            TimeAxisDirection.Vertical => new(x, 0),
            _ => Offset.Invalid
        };
    }
}
