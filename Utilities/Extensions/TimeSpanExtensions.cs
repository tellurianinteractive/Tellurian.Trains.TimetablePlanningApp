using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TimetablePlanning.Utilities.Extensions;
public static class TimeSpanExtensions
{
    public static TimeSpan Max(this TimeSpan time, TimeSpan other ) => time > other ? time : other;
    public static TimeSpan Min(this TimeSpan time, TimeSpan other) => time < other ? time : other;

}
