using System.Collections.Generic;
using System.Linq;

namespace Tellurian.Trains.Scheduling.Model
{
    public static class TimetableBuilder
    {
        public static TimetableStretch Bohusbanan => new("Södra Bohusbanan") { TrackStreches = Stations.Stretches().ToArray() };

        public static IEnumerable<TrackStretch> Stretches(this IEnumerable<Station> stations)
        {
            var ss = stations.ToArray();
            for (var i = 1; i < ss.Length; i++)
            {
                yield return new TrackStretch(ss[i - 1], ss[i]);
            }
        }

        public static IEnumerable<Station> Stations => new Station[]
        {
            new ("Uddevalla C", "Uv") { Km = 89, Tracks = new StationTrack[] { new("1"), new("2"), new("3") } },
            new ("Uddevalla Ö", "Uö") { Km = 87, Tracks = new StationTrack[] { new("1")} },
            new ("Grohed", "Gro") { Km = 75, Tracks = new StationTrack[] { new("1") {HasPlatform=false }, new("2") { HasPlatform = false } } },
            new ("Ljungskile", "Lj") { Km = 67 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Svenshögen", "Svg") { Km = 60 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Stenungsund", "Snu") { Km = 48 , Tracks = new StationTrack[] { new("1b"), new("2"), new StationTrack("3") { HasPlatform=false }, new StationTrack("4") {IsScheduled=false, HasPlatform=false } } },
            new ("Stora Höga", "Sth") { Km = 41 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Kode", "Kde") { Km = 33 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Ytterby", "Yb") { Km = 22 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Säve", "Sve") { Km = 15, Tracks = new StationTrack[] { new("1"), new("2") } },
            new ("Göteborg Kville", "Gk") { Km = 4, Tracks = new StationTrack[] { new("1") { HasPlatform = false }, new("2"){ HasPlatform=false } }},
            new ("Olskroken", "Or") { Km = 2 ,Tracks = new StationTrack[] { new("1") { HasPlatform = false }, new("2"){ HasPlatform=false } }},
            new ("Göteborg C", "G") { Km = 0,Tracks = new StationTrack[] { new("6"), new("7"), new("8"),new("9"), new("10"), new("11") }}
        };
    }
}
