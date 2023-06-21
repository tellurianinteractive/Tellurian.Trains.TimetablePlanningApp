namespace TimetablePlanning.Models.Common;

public class Station
{
    public Station(string name, string signature)
    {
        Name = name;
        Signature = signature;
    }
    public string Name { get; }
    public string Signature { get;  }
    public int Km { get; init; }
    public StationTrack[] Tracks { get { return _Tracks; } init { _Tracks = value; UpdateTrackReferences(); } }

    private StationTrack[] _Tracks = Array.Empty<StationTrack>();

    public override string ToString() => Signature;

    private void UpdateTrackReferences()
    {
        foreach (var track in Tracks) track.Station = this;
    }
    public override bool Equals(object? obj) => obj is Station other && other.Signature == Signature;
    public override int GetHashCode() => Signature.GetHashCode();
}

public static class StationExtensions
{
    public static int StationTrackIndex(this Station me, string number) => me.Tracks.IndexOf( x => x.Number.Equals(number, StringComparison.OrdinalIgnoreCase));
    public static bool HasPassengerExchange(this Station me) => me.Tracks.Any(t => t.HasPlatform);

}


public static class LinqExtensions
{
    public static int IndexOf<T>(this IEnumerable<T> it, Func<T, bool> predicate) =>
        !it.Any() ? -1 :
        it.Select((item, index) => new { item, index }).FirstOrDefault(x => predicate(x.item))?.index ?? -1;
}