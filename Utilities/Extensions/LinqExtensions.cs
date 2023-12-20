namespace TimetablePlanning.Utilities.Extensions;

public static class LinqExtensions
{
    public static int IndexOf<T>(this IEnumerable<T> it, Func<T, bool> predicate) =>
        !it.Any() ? -1 :
        it.Select((item, index) => new { item, index }).FirstOrDefault(x => predicate(x.item))?.index ?? -1;
}