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
    public static string DivText(this string text, string after=" ") => Div(text, "text", after);
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
        Span(origin.FullName, "origin");

    public static string Span(this string? text, string cssClass = "text", string after = "") =>
       $"""
        <span class="callnote {cssClass}">{text ?? string.Empty}{after} </span>
        """;
    public static string Div(this string? text, string cssClass = "", string after = "") =>
        string.IsNullOrEmpty(cssClass) ?
       $"""
        
        <div class="callnote">{text ?? string.Empty}{after}</div>
        """:
       $"""
        
        <div class="callnote {cssClass}">{text ?? string.Empty}{after}</div>
        """;
}
