using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public sealed class TrainInfo
{
    public string? OperatorSignature { get; init; }
    public string? Prefix { get; init; }
    public required int Number { get; init; }
    public required OperationDays OperationDays { get; init; }

    public override string ToString() => $"{OperatorSignature} {Prefix} {Number}".TrimStart();
}
