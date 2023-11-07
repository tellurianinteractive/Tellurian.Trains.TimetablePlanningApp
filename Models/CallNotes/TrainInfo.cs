using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public sealed class TrainInfo
{
    public string? OperatorSignature { get; init; }
    public required string Number { get; init; }
    public required OperationDays OperationDays { get; init; }

    public override string ToString() => $"{OperatorSignature} {Number}".TrimStart();
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
