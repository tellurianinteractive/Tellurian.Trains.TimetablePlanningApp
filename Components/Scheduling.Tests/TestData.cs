using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling.Tests;

public static class TestData
    {
        public static Station Station1 => new("Stenungsund", "Snu") { Km = 48, Tracks = StationTracks };

        private static StationTrack[] StationTracks => new[]
        {
            new StationTrack("1b" ) { HasPlatform=true },
            new StationTrack("1a" ) { HasPlatform=true },
            new StationTrack("2" ) { HasPlatform=true },
            new StationTrack("3" ) { HasPlatform=false },
            new StationTrack("4" ) { HasPlatform=false },
       };
    }
