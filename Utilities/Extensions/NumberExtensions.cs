using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TimetablePlanning.Utilities.Extensions;
public static class NumberExtensions {
    public static bool BothIsOddOrEven(this int number, int otherNumber) => (number - otherNumber) % 2 == 0;
}
