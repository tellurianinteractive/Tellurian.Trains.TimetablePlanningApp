namespace TimetablePlanning.Models.Common;

public record Train(string Number)
{
    public string Colour { get; set; } = "black";
    public StationCall[] Calls => _Calls.ToArray();
    private readonly List<StationCall> _Calls = new();

    public void Add(StationCall call) => _Calls.Add(call);
    public void Add(IEnumerable<StationCall> calls) => _Calls.AddRange(calls);  

}
