using System.Globalization;

namespace TimetablePlanning.Models.CallNotes.Tests;
internal class TestHelpers
{
    public static void SetTestLanguage(string language = "en-GB")
    {
        var culture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}
