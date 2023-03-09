namespace TimetablePlanning.Models.CallNotes.Extensions;

/// <summary>
/// This class holds all HTML/CSS formatting for notes.
/// </summary>
internal static class MarkupExtensions
{
    public static string ErrorMarkup(this string text) => new(Markup(text, "error"));
    public static string ItemMarkup(this string text) => Markup(text, "item");
    public static string TextMarkup(this string text) => Markup(text, "text");
    public static string DaysMarkup(this string text) => Markup(text, "days");

    public static string Markup(this string text, string cssClass = "text") =>
       $"""
        <span class="note-{cssClass}">{text} </span>
        """;
    
}
