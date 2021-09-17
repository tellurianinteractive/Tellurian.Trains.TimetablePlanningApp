using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tellurian.Trains.Scheduling.Extensions;


namespace Tellurian.Trains.Scheduling.Model
{
    public record GraphScheduleModel(GraphStation[] Stations, GraphTime[] Times)
    {

    }

    public static class GraphScheduleModelExtensions
    {
        public static GraphScheduleModel AsGraphScheduleModel(this TimetableStretch me)
        {
            var stations = new List<GraphStation>(20);
            for (var s = 0; s < me.Stations.Length; s++)
            {
                var station = me.Stations[s];
                var tracks = new List<GraphTrack>(50);
                for (var t = 0; t < station.Tracks.Length; t++)
                {
                    var track = station.Tracks[t];
                    var start = me.TrackStartLocation(s, t);
                    var end = me.TrackEndLocation(s, t);
                    var graphicalTrack = new GraphTrack(start, end, station, track, AsInputArea(start, end, (me.Settings.TrackSpacing / 2) - 1)) ;
                    tracks.Add(graphicalTrack);
                }
                stations.Add(new GraphStation(me.StationLabelOffset(s), me.StationLabelOffset(s), station, tracks.ToArray()));
            }
            return new GraphScheduleModel(stations.ToArray(), Times(me));
        }

        static GraphTime[] Times(TimetableStretch me)
        {
            var times = new List<GraphTime>(24);
            int start = me.StartTime.Hours;
            int end = me.EndTime.Minutes > 0 || me.EndTime.Seconds > 0 ? me.EndTime.Hours + 1 : me.EndTime.Hours;
            for (var hour = start; hour <= end; hour++)
            {
                var (Start, End) = me.TimeLine(TimeSpan.FromHours(hour));
                times.Add(new GraphTime(Start, End, hour));
            }
            return times.ToArray();
        }


        static GraphInputArea AsInputArea(Offset start, Offset end, int discrimination) =>
            start.X == end.X ?
                new(new(start.X - discrimination, start.Y), new(end.X + discrimination, end.Y)) :
                new(new(start.X, start.Y - discrimination), new(end.X, end.Y + discrimination));

    }

    public record GraphTime(Offset Start, Offset End, int Hour);

    public record GraphStation(Offset Start, Offset End, Station Station, GraphTrack[] GraphicTracks);

    public record GraphTrack(Offset Start, Offset End, Station Station, StationTrack StationTrack, GraphInputArea InputArea);
    

    public record GraphInputArea(Offset Min, Offset Max)
    {
        public bool IsWithin(Offset location) => location.X >= Min.X && location.Y >= Min.Y && location.X <= Max.X && location.Y <= Max.Y;
    }

}
