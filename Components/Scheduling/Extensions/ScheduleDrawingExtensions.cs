using System.Runtime.CompilerServices;
using TimetablePlanning.Models.Common;

[assembly: InternalsVisibleTo("TimetablePlanning.Scheduling.Tests")]
namespace TimetablePlanning.Components.Scheduling.Extensions;

public static class ScheduleDrawingExtensions
{
    public static string OrientationCss(this TimeAxisDirection axisDirection, string classes) =>
        axisDirection == TimeAxisDirection.Horisontal ? $"{classes} horizontal".TrimStart() :
        axisDirection == TimeAxisDirection.Vertical ? $"{classes} vertical".TrimStart() :
        string.Empty;


    public static (Offset Start, Offset End) TrainBetweenStationsLine(this Schedule me, int departureStationIndex, int departureTrackIndex, int arrivalStationIndex, int arrivalTrackIndex, CallAction departure, CallAction arrival)
    {
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var yd = me.Y(departureStationIndex, departureTrackIndex);
            var ya = me.Y(arrivalStationIndex, arrivalTrackIndex);
            return (new(me.TimeOffset(departure.Time).X, yd), new(me.TimeOffset(arrival.Time).X, ya));

        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var xd = me.X(departureStationIndex, departureTrackIndex);
            var xa = me.X(arrivalStationIndex, arrivalTrackIndex);
            return (new(xd, me.TimeOffset(departure.Time).Y), new(xa, me.TimeOffset(arrival.Time).Y));

        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }


    public static (Offset Start, Offset End) TrainAtStationLine(this Schedule me,int stationIndex, int trackIndex, StationCall stationCall)
    {
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var y = me.Y( stationIndex, trackIndex);
            return (new(me.TimeOffset( stationCall.Arrival.Time).X, y), new(me.TimeOffset( stationCall.Departure.Time).X, y));

        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var x = me.X( stationIndex, trackIndex);
            return (new(x, me.TimeOffset( stationCall.Arrival.Time).Y), new(x, me.TimeOffset( stationCall.Departure.Time).Y));

        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }

    public static Offset ArrivalMinuteOver(this Schedule me, int stationIndex, StationCall stationCall)
    {
        var offset = me.MinuteOver(stationIndex, stationCall.Arrival);
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new Offset(offset.X - 13, offset.Y - 2),
            TimeAxisDirection.Vertical => new Offset(offset.X-10, offset.Y-6),
            _ => throw new NotSupportedException(me.AxisDirection.ToString())
        };
    }

    public static Offset ArrivalMinuteUnder(this Schedule me, int stationIndex, int trackIndex, StationCall stationCall)
    {
        var offset = me.MinuteUnder( stationIndex, trackIndex, stationCall.Arrival);
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new Offset(offset.X - 16, offset.Y + 7),
            TimeAxisDirection.Vertical => new Offset(offset.X +2, offset.Y-7),
            _ => throw new NotSupportedException(me.AxisDirection.ToString())
        };
    }
    public static Offset DepartureMinuteOver(this Schedule me, int stationIndex, StationCall stationCall)
    {
        var offset = me.MinuteOver( stationIndex, stationCall.Departure);
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new Offset(offset.X + 4, offset.Y - 2),
            TimeAxisDirection.Vertical => new Offset(offset.X-10, offset.Y+10),
            _ => throw new NotSupportedException(me.AxisDirection.ToString())
        };
    }
    public static Offset DepartureMinuteUnder(this Schedule me, int stationIndex, int trackIndex, StationCall stationCall)
    {
        var offset = me.MinuteUnder(stationIndex, trackIndex, stationCall.Departure);
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new Offset(offset.X + 4, offset.Y + 8),
            TimeAxisDirection.Vertical => new Offset(offset.X+1, offset.Y+11),
            _ => throw new NotSupportedException(me.AxisDirection.ToString())
        };
    }

    private static Offset MinuteUnder(this Schedule me, int stationIndex, int trackIndex, CallAction callAction)
    {
        var station = me.Stations[stationIndex];
        var lastTrackIndex = station.Tracks.Length - 1;
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.TimeOffset(callAction.Time).X + (lastTrackIndex - trackIndex);
            var y = me.Y(stationIndex, lastTrackIndex);
            return new Offset(x, y);
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var x = me.X(stationIndex, lastTrackIndex);
            var y = me.TimeOffset(callAction.Time).Y + (lastTrackIndex - trackIndex);
            return new Offset(x, y);

        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }

    private static Offset MinuteOver(this Schedule me, int stationIndex, CallAction callAction)
    {
        var trackIndex = 0;
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.TimeOffset(callAction.Time).X;
            var y = me.Y(stationIndex, trackIndex);
            return new Offset(x, y);
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var x = me.X(stationIndex, trackIndex);
            var y = me.TimeOffset(callAction.Time).Y;
            return new Offset(x, y);

        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }



    public static (Offset Start, Offset End) TrackLine(this Schedule me, int stationIndex, int trackIndex)
    {
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var y = me.Y( stationIndex, trackIndex);
            return (new(me.GraphSettings.KilometerAxisSpacing.X, y), new(me.MaxTimeOffset().X, y));
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var x = me.X( stationIndex, trackIndex);
            return (new(x, me.GraphSettings.KilometerAxisSpacing.Y), new(x, me.MaxTimeOffset().Y));
        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }

    public static (Offset Start, Offset End) TimeLine(this Schedule me, TimeSpan time)
    {
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.TimeOffset(time).X;
            return (new(x, me.GraphSettings.TimeAxisSpacing.Y), new(x, me.MaxTrackOffset().Y));
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var y = me.TimeOffset(time).Y;
            return (new(me.GraphSettings.TimeAxisSpacing.X, y), new(me.MaxTrackOffset().X, y));
        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }

    public static int Height(this Schedule me) =>
        me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => me.MaxTrackOffset().Y + me.GraphSettings.EndMargin,
            TimeAxisDirection.Vertical => me.MaxTimeOffset().Y + me.GraphSettings.EndMargin,
            _ => 0
        };

    public static int Width(this Schedule me) =>
        me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => me.MaxTimeOffset().X + me.GraphSettings.EndMargin,
            TimeAxisDirection.Vertical => me.MaxTrackOffset().X + me.GraphSettings.EndMargin,
            _ => 0
        };

    public static Offset TimeAxisLabelOffset(this Schedule me, TimeSpan time) =>
       me.AxisDirection switch
       {
           TimeAxisDirection.Horisontal => me.TimeLine( time).Start - new Offset(5, 5),
           TimeAxisDirection.Vertical => me.TimeLine( time).Start - new Offset(me.GraphSettings.TimeAxisSpacing.X - 5, 0),
           _ => throw new NotSupportedException()
       };

    public static Offset StationLabelOffset(this Schedule me, int stationIndex)
    {
        var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.GraphSettings.TrackSpacing;
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new(5, me.Y(stationIndex, 0) + offset),
            TimeAxisDirection.Vertical => new(me.X(stationIndex, 0) + offset, 25),
            _ => Offset.Invalid
        };
    }

    public static Offset KmLabelOffset(this Schedule me, int stationIndex)
    {
        var offset = me.Stations[stationIndex].Tracks.Length / 2 * me.GraphSettings.TrackSpacing;
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new(me.GraphSettings.KilometerAxisSpacing.X - 15, me.Y(stationIndex, 0) + offset),
            TimeAxisDirection.Vertical => new(me.X( stationIndex, 0) + offset, me.GraphSettings.KilometerAxisSpacing.Y - 15),
            _ => Offset.Invalid
        };
    }

    public static Offset TrackNumberOffset(this Schedule me, int stationIndex, int trackIndex)
    {
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new(me.GraphSettings.KilometerAxisSpacing.X - 2, me.Y( stationIndex, trackIndex) + 3),
            TimeAxisDirection.Vertical => new(me.X(stationIndex, trackIndex) + 0, me.GraphSettings.KilometerAxisSpacing.Y - 2),
            _ => Offset.Invalid
        };

    }

    public static TimeSpan? Time(this Schedule me, int xOffset, int yOffset)
    {
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var x = xOffset - me.GraphSettings.KilometerAxisSpacing.X;
            var time = TimeSpan.FromMinutes(x / me.GraphSettings.MinuteSpacing).Add(me.StartTime);
            return time >= me.StartTime && time <= me.EndTime ? time : null;
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var y = yOffset - me.GraphSettings.KilometerAxisSpacing.Y;
            var time = TimeSpan.FromMinutes(y / me.GraphSettings.MinuteSpacing).Add(me.StartTime);
            return time >= me.StartTime && time <= me.EndTime ? time : null;
        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }


    public static Offset TimeOffset(this Schedule me, TimeSpan time)
    {
        if (time < me.StartTime || time > me.EndTime) return Offset.Invalid;
        if (me.AxisDirection == TimeAxisDirection.Horisontal)
        {
            var x = me.GraphSettings.KilometerAxisSpacing.X + (me.GraphSettings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
            return new(x, 0);
        }
        else if (me.AxisDirection == TimeAxisDirection.Vertical)
        {
            var y = me.GraphSettings.KilometerAxisSpacing.Y + (me.GraphSettings.MinuteSpacing * (int)(time - me.StartTime).TotalMinutes);
            return new(0, y);
        }
        throw new NotSupportedException(me.AxisDirection.ToString());
    }

    public static Offset MaxTimeOffset(this Schedule me) =>
        TimeOffset(me, me.EndTime);

    public static int X(this Schedule me, int stationIndex, int trackIndex) =>
        me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => TrackOffset(me, stationIndex, trackIndex).Y,
            TimeAxisDirection.Vertical => TrackOffset(me, stationIndex, trackIndex).X,
            _ => 0
        };

    public static int Y(this Schedule me, int stationIndex, int trackIndex) =>
         me.AxisDirection switch
         {
             TimeAxisDirection.Horisontal => TrackOffset(me, stationIndex, trackIndex).Y,
             TimeAxisDirection.Vertical => TrackOffset(me, stationIndex, trackIndex).X,
             _ => 0
         };

    public static Offset TrackStartLocation(this Schedule me, int stationIndex, int trackIndex) =>
          me.TrackOffset(stationIndex, trackIndex) + me.TimeOffset(me.StartTime);

    public static Offset TrackEndLocation(this Schedule me,int stationIndex, int trackIndex) =>
         me.TrackOffset(stationIndex, trackIndex) + me.TimeOffset(me.EndTime);

    public static Offset TrackOffset(this Schedule me, int stationIndex, int trackIndex)
    {
        var x = me.GraphSettings.TimeAxisSpacing.X;
        var y = me.GraphSettings.TimeAxisSpacing.Y;
        if (stationIndex == 0)
        {
            y += me.GraphSettings.TrackSpacing * trackIndex;
            x += me.GraphSettings.TrackSpacing * trackIndex;
        }
        else
        {
            for (var i = 0; i < stationIndex; i++)
            {
                var stretch = me.Streches[i];
                var Δ1 = Math.Max(me.GraphSettings.MinStationSpacing, ((stretch.From.Tracks.Length - 1) * me.GraphSettings.TrackSpacing) + (me.GraphSettings.KilometerSpacing * stretch.Km));
                x += Δ1;
                y += Δ1;
            }
            var Δ2 = trackIndex * me.GraphSettings.TrackSpacing;
            x += Δ2;
            y += Δ2;
        }
        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new(0, y),
            TimeAxisDirection.Vertical => new(x, 0),
            _ => Offset.Invalid
        };
    }

    public static Offset MaxTrackOffset(this Schedule me)
    {
        var x = me.GraphSettings.TimeAxisSpacing.X + ((me.Streches[0].From.Tracks.Length - 1) * me.GraphSettings.TrackSpacing);
        var y = me.GraphSettings.TimeAxisSpacing.Y + ((me.Streches[0].From.Tracks.Length - 1) * me.GraphSettings.TrackSpacing);
        for (var i = 0; i < me.Streches.Length; i++)
        {
            var stretch = me.Streches[i];
            var Δ = Math.Max(me.GraphSettings.MinStationSpacing, (me.GraphSettings.KilometerSpacing * stretch.Km) + ((stretch.To.Tracks.Length - 1) * me.GraphSettings.TrackSpacing));
            x += Δ;
            y += Δ;
        }

        return me.AxisDirection switch
        {
            TimeAxisDirection.Horisontal => new(0, y),
            TimeAxisDirection.Vertical => new(x, 0),
            _ => Offset.Invalid
        };
    }
}
