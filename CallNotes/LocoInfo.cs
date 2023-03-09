namespace TimetablePlanning.Models.CallNotes;

using TimetablePlanning.Utilities.Extensions;

public class LocoInfo
{
    public required string OperatorSignature { get; init; }
    public required string Class { get; init; }
    public required string TurnusNumber { get; init; }
    public string LocoNumber { get; init; } = string.Empty;
    public required OperationDays OperationDays { get; init; }
    public bool IsDoubleDirectionTrain { get; init; }
    public bool IsSingleDirectionTractionUnit => !IsDoubleDirectionTrain;
    public override string ToString() => ToString(' ');
    public  string ToString(char punctuation = ' ') =>
        LocoNumber.HasValue() ? $"{OperatorSignature} {Class} {LocoNumber} {Resources.Notes.Turn} {TurnusNumber}{punctuation}".Trim() :
        $"{OperatorSignature} {Class} {Resources.Notes.Turn} {TurnusNumber}{punctuation}".Trim();
}


