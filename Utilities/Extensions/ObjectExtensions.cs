namespace TimetablePlanning.Utilities.Extensions;
public static class ObjectExtensions
{
    public static T[] AsArray<T>(this T item) => [item];
    public static IEnumerable<T> AsEnumerable<T>(this T item) => new T[] { item };

}
