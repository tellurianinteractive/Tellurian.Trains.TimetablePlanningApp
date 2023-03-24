using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
