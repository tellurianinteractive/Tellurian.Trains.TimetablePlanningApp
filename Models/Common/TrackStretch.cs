namespace TimetablePlanning.Models.Common;

public record TrackStretch(Station From, Station To)
{
    public int Km => Math.Abs(From.Km - To.Km);

    /// <summary>
    /// Number of tracks: 1=single track, 2=double track etc.
    /// Denotes how many trains that can be on the track stretch at the same time.
    /// </summary>
    public int NumberOfTracks { get; init; } = 1;
}

