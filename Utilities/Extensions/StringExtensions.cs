using System.Diagnostics.CodeAnalysis;

namespace TimetablePlanning.Utilities.Extensions;

public static class StringExtensions
{
    public static string[] AsArray(this string? value) =>
        value is null ? Array.Empty<string>() : new string[] { value };

    public static bool HasValue([NotNullWhen(true)] this string? me) =>
        !string.IsNullOrWhiteSpace(me);

    public static bool HasNoValue([NotNullWhen(false)] this string? me) =>
        string.IsNullOrWhiteSpace(me);

    public static string FirstItem(this string? me, string defaultValue = "") =>
        me is null || me.Length == 0 ? defaultValue : me.Split(',')[0] ?? defaultValue;

    public static  string MarkupSpan(this string text, string cssClass = "text") =>
        $"""
        <span class="note-{cssClass}">{text} </span>
        """;

}
