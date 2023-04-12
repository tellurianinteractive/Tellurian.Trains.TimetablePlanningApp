using System.Globalization;
using System.Linq;

namespace TimetablePlanning.App.Shared.Services;


public class LanguageService
{
    public static readonly CultureInfo[] SupportedCultures = new CultureInfo[]
    {
        new CultureInfo("en"),
        new CultureInfo("da"),
        new CultureInfo("de"),
        new CultureInfo("nb"),
        new CultureInfo("sv"),
    };

    public static string[] Languages => SupportedCultures.Select(c => c.TwoLetterISOLanguageName).ToArray();
    public static readonly string DefaultLanguage = SupportedCultures[0].TwoLetterISOLanguageName;
}

