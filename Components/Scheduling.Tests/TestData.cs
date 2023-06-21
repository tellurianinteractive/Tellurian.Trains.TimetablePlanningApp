using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling.Tests;

public static class TestData
    {
        public static Station Station1 => new("Stenungsund", "Snu") { Km = 48, Tracks = StationTracks };

        private static StationTrack[] StationTracks => new[]
        {
            new StationTrack("1b" ) {PlatformLength = 2},
            new StationTrack("1a" ) {PlatformLength = 2 },
            new StationTrack("2" ) { PlatformLength = 2 },
            new StationTrack("3" ),
            new StationTrack("4" ) ,
       };
    }
