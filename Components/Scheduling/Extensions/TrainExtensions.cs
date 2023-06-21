using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling.Extensions;

internal static class TrainExtensions
{
    public static IEnumerable<StretchUse> StretchUses(this Train train)
    {
        for (var c = 1; c < train.Calls.Length; c++)
        {
            yield return new StretchUse(train.Calls[c-1], train.Calls[c]);
        }
    }
}
