namespace TimetablePlanning.Models.Common;

public record Train(string Number)
{
    public string Colour { get; set; } = "black";
    public StationCall[] Calls => [.. _Calls];
    private readonly List<StationCall> _Calls = [];

    public void Add(StationCall call) => _Calls.Add(call);
    public void Add(IEnumerable<StationCall> calls) => _Calls.AddRange(calls);  

}
