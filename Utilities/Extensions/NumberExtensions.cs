namespace TimetablePlanning.Utilities.Extensions;
public static class NumberExtensions {
    public static bool BothIsOddOrEven(this int number, int otherNumber) => (number - otherNumber) % 2 == 0;
}
