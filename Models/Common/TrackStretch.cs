namespace TimetablePlanning.Models.Common;

public record TrackStretch(Station From, Station To)
{
    public int Km => Math.Abs(From.Km - To.Km);
}

