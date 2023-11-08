using System.Globalization;
using System.Numerics;
using System.Text;

namespace TimetablePlanning.Models.Common;

public class OperationDays
{
    public required string FullName { get; init; }
    public required string ShortName { get; init; }

    public bool IsDaily => Flags == OperationDayFlags.Daily;
    public bool IsSingleDay => NumberOfDays == 1;
    public bool IsOnDemand => Flags == OperationDayFlags.OnDemand;
    public int NumberOfDays { get; init; }
    public required byte Flags;

    public override bool Equals(object? obj) => obj is OperationDays other && other.Flags == Flags;
    public override int GetHashCode() => ShortName.GetHashCode(StringComparison.OrdinalIgnoreCase);
    public override string ToString() => ShortName;

    public static OperationDays Daily => OperationDayFlags.Daily.ToOperationDays();
    public static OperationDays OnDemand => OperationDayFlags.OnDemand.ToOperationDays();

   public static OperationDays operator &(OperationDays days1, OperationDays days2) => 
        days1.IsOnDemand || days2.IsOnDemand ? OnDemand : 
        ((byte)(days1.Flags & days2.Flags)).ToOperationDays();

    public static bool operator == (OperationDays one, OperationDays another) => one.Equals(another);
    public static bool operator !=(OperationDays one, OperationDays another) => !one.Equals(another);

    public bool IsAllOtherDays(OperationDays operationDays) => And(Flags) == operationDays.Flags;
    public bool IsAnyOtherDays(OperationDays operationDays) => And(operationDays.Flags) > 0;
    public bool IsNoneOtherDays(OperationDays operationDays) => And(operationDays.Flags) == 0;
    private byte And(byte otherFlags) => IsOnDemand ? Flags : (byte)(Flags & otherFlags);
    public int DisplayOrder => ~Flags;
}

public static class OperationDayFlags
{
    public const byte Daily = 0x7f;
    public const byte MoWeFr = 0b00010101;
    public const byte OnDemand = 0x80;
    public const byte TuThSa = 0b00101010;
}

public static class OperationDaysExtensions
{

    private static readonly Day[] Days = [
            new Day(0, 0x7F, "Daily"),
            new Day(1, 0x01, "Monday"),
            new Day(2, 0x02, "Tuesday"),
            new Day(3, 0x04, "Wednesday"),
            new Day(4, 0x08, "Thursday"),
            new Day(5, 0x10, "Friday"),
            new Day(6, 0x20, "Saturday"),
            new Day(7, 0x40, "Sunday"),
            new Day(0, 0x80, "OnDemand") ];

    private static Day[] GetDays(this byte flags) =>
        flags == Days[0].Flag ? new Day[] { Days[0] } :
        flags == Days[8].Flag ? new Day[] { Days[8] } :
        Days.Where(d => d.Number > 0 && (d.Flag & flags) > 0).ToArray();

    public static OperationDays ToOperationDays(this byte flags)
    {
        var days = GetDays(flags);
        var fullName = new StringBuilder(20);
        var shortName = new StringBuilder(10);
        if (days.Length == 0)
        {
            return new OperationDays { FullName = "", ShortName = "", NumberOfDays = 0, Flags=flags };
        }
        if (days.Length == 1)
        {
            fullName.Append(days[0].FullName);
            shortName.Append(days[0].ShortName);
        }
        else
        {
            var dayNumber = 0;
            var lastDayNumber = days.Last().Number;
            if (days.IsConsecutiveFromMonday())
            {
                Append(days[0], fullName, shortName);
                Append(Resources.Days.To.ToLowerInvariant(), "-", fullName, shortName);
                Append(days.Last(), fullName, shortName, true);
            }
            else if (days.IsConsecutiveFromSunday())
            {
                Append(days[^1], fullName, shortName);
                Append(Resources.Days.To.ToLowerInvariant(), "-", fullName, shortName);
                Append(days[^2], fullName, shortName, true);

            }
            else if (flags == 0x5F)
            {
                Append(Days[1], fullName, shortName);
                Append(Resources.Days.To.ToLowerInvariant(), "-", fullName, shortName);
                Append(Days[5], fullName, shortName, true);
                Append(Resources.Days.And.ToLowerInvariant(), ",", fullName, shortName);
                Append(Days[7], fullName, shortName, true);
            }
            else if (flags == 0x4F)
            {
                Append(Days[7], fullName, shortName);
                Append(Resources.Days.To.ToLowerInvariant(), "-", fullName, shortName);
                Append(Days[4], fullName, shortName, true);
            }
            else
            {
                foreach (var day in days)
                {
                    if (day.Number == lastDayNumber)
                    {
                        Append(Resources.Days.And.ToLowerInvariant(), ",", fullName, shortName);
                    }
                    else if (dayNumber > 0)
                    {
                        Append(",", ",", fullName, shortName);
                    }
                    Append(day, fullName, shortName, day.Number > days[0].Number);
                    dayNumber = day.Number;
                }
            }
        }
        return new OperationDays
        {
            NumberOfDays = days.Length,
            FullName = fullName.ToString(),
            ShortName = shortName.ToString(),
            Flags = flags,
        };
    }
    private static void Append(Day day, StringBuilder fullNames, StringBuilder shortNames, bool toLower = false)
    {
        _ = fullNames.Append(toLower && Resources.Days.DayNameCasing.Equals("LOWER", StringComparison.OrdinalIgnoreCase) ? day.FullName.ToLowerInvariant() : day.FullName);
        shortNames.Append(day.ShortName);
    }
    public static void Append(this string fullText, string shortText, StringBuilder fullNames, StringBuilder shortNames)
    {
        if (fullText.Length > 1) fullNames.Append(' ');
        fullNames.Append(fullText);
        fullNames.Append(' ');
        shortNames.Append(shortText);
    }

    #region WORK IN PROGRESS For checking overlapping days

    public static int OneBitsCount(this byte it)
    {
        ReadOnlySpan<byte> nibbleLookup = new byte[] { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4 };
        return nibbleLookup[it & 0x0F] + nibbleLookup[it >> 4];
    }

    public static bool IsBitSet(this byte it, int bit) => bit >= 1 && bit <= 8 && (it & Bit(bit)) > 0;
    public static byte Bit(int bit) => (byte)(Math.Pow(2, bit - 1));
    public static byte ZeroBit(this byte it, int bit) => (byte)(it & (~(byte)(1 << (bit - 1))));

    #endregion
}

internal class Day(byte number, byte flag, string resourceKey)
{
    public byte Flag { get; } = flag;
    public byte Number { get; } = number;
    private string FullNameResourceKey { get; } = resourceKey;
    private string ShortNameResourceKey { get; } = resourceKey + "Short";
    public string FullName => Resources.Days.ResourceManager.GetString(FullNameResourceKey, CultureInfo.CurrentCulture) ?? FullNameResourceKey;
    public string ShortName => Resources.Days.ResourceManager.GetString(ShortNameResourceKey, CultureInfo.CurrentCulture) ?? ShortNameResourceKey;
}

internal static class DayExtensions
{
    public static bool IsConsecutiveFromMonday(this Day[] days) =>
        days.Length == days.Last().Number - days[0].Number + 1;

    public static bool IsConsecutiveFromSunday(this Day[] days) =>
        days.Length >= 3 && days.Last().Number == 7 && days[0..^2].IsConsecutiveFromMonday();
}

