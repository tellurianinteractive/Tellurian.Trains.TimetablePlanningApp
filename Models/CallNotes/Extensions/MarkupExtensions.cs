using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes.Extensions;

/// <summary>
/// This class holds all HTML/CSS formatting for notes.
/// </summary>
internal static class MarkupExtensions
{
    public static string ErrorMarkup(this string text) => new(Span(text, "error"));
    public static string SpanValue(this string text) => Span(text, "value");
    public static string SpanText(this string text) => Span(text, "text");
    public static string SpanDays(this string text) => Span(text, "days", ":");
    public static string SpanDestination(this DestinationInfo destination, int numberOfWagons =0) =>
        numberOfWagons > 0 ?
        $"""
        <span class="note-destination" style="color: {destination.ForeColor};background-color: {destination.BackColor};">{destination.FullName} × {numberOfWagons}</span>
        """ :
        $"""
        <span class="note-destination" style="color: {destination.ForeColor};background-color: {destination.BackColor};">{destination.FullName}</span>
        """;
    public static string SpanOrigin(this OriginInfo origin) =>
        Span(origin.FullName, "origon");

    public static string Span(this string text, string cssClass = "text", string after = "") =>
    $"""
        <span class="note-{cssClass}">{text}{after} </span>
        """;

    public static string Div(this string? text, string cssClass = "item") =>
        text.HasNoValue() ? string.Empty :
        $"""
        <div class="note-{cssClass}">{text}</div>
        """;

}
