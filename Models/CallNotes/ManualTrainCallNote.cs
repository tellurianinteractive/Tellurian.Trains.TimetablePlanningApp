using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace TimetablePlanning.Models.CallNotes;
public class ManualTrainCallNote : TrainCallNote
{
    public override MarkupString Markup()
    {
        var currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        if (_localizedTexts.TryGetValue(currentLanguage, out string? value))
        {
            return new MarkupString(value);
        }
        else if (_localizedTexts.Count > 0)
        {
            return new MarkupString(_localizedTexts.Values.First());
        }
        else
        {
            return new MarkupString(string.Empty);
        }
    }

    private readonly Dictionary<string, string> _localizedTexts = [];
    public void Add(string text, string twoLetterIsoLanguageName) => 
        _localizedTexts.Add(twoLetterIsoLanguageName, text);
}



