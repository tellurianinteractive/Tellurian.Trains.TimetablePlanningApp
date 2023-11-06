using System.Globalization;

namespace TimetablePlanning.Models.CallNotes.Tests;
internal static class TestHelpers
{
    public static void IsTestLanguage(this string language)
    {
        var culture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}
