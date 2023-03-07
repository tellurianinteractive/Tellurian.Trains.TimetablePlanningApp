using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetablePlanning.Utilities.Extensions;
public static class ObjectExtensions
{
    public static T[] AsArray<T>(this T item) => new T[] { item };
    public static IEnumerable<T> AsEnumerable<T>(this T item) => new T[] { item };

}
