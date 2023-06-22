using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling.Extensions;

internal static class TrainExtensions
{
    public static IEnumerable<StretchUse> StretchUses(this Train train)
    {
        for (var callIndex = 0; callIndex < train.Calls.Length - 1; callIndex++)
        {
            yield return new StretchUse(train, callIndex);
        }
    }
}
