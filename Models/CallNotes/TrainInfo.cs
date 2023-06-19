using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public sealed class TrainInfo
{
    public string? OperatorSignature { get; init; }
    public string? Prefix { get; init; }
    public required int Number { get; init; }
    public required OperationDays OperationDays { get; init; }

    public override string ToString() => $"{OperatorSignature} {Prefix} {Number}".TrimStart();
    public string Markup => ToString().SpanValue();
}

public sealed class TrainCallInfo
{
    public required TimeSpan ArrivalTime { get; init; }
    public required TimeSpan DepartureTime { get; init; }
    public bool IsStop { get; init; }

    public bool IsOverlapping(TrainCallInfo callInfo) =>
        ArrivalTime <= callInfo.DepartureTime && DepartureTime >= callInfo.ArrivalTime;
}
