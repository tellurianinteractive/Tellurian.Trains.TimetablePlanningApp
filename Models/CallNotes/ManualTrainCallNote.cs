using Microsoft.AspNetCore.Components;
using System.Globalization;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;
public class ManualTrainCallNote : TrainCallNote
{
    public required OperationDays DisplayDays { get; set; }  
    protected override OperationDays NoteDays => DisplayDays;
    public override MarkupString Markup() => new(MarkupText);

    private string MarkupText => 
        $"""
        <span class="callnote text">{LocalizedText}</span>
        """.Div();

    private string LocalizedText
    {
        get
        {
            var currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (_localizedTexts.TryGetValue(currentLanguage, out string? value))
            {
                return value;
            }
            else if (_localizedTexts.Count > 0)
            {
                return _localizedTexts.Values.First();
            }
            else
            {
                return string.Empty;
            }

        }
    }


    private readonly Dictionary<string, string> _localizedTexts = [];
    public void Add(string text, string twoLetterIsoLanguageName) =>
        _localizedTexts.Add(twoLetterIsoLanguageName, text);
}



