using System.Data;
using System.Globalization;
using System.Resources;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;


public static class IDataRecordExtensions
{
    private const bool ThrowOnColumnError = true; // Set true only for debugging
    public static string GetString(this IDataRecord me, string columnName, string? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, defaultValue is null);
        if (i < 0)
        {
            if (defaultValue is null) throw TypeErrorException(defaultValue, columnName);
            else return defaultValue;
        }
        if (me.IsDBNull(i)) return string.Empty;
        try
        {
            var s = me.GetString(i);
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s;
        }
        catch (Exception)
        {
            throw TypeErrorException(defaultValue, columnName);
        }
    }

    public static string GetStringResource(this IDataRecord me, string columnName, ResourceManager resourceManager, string defaultValue = "")
    {
        var resourceKey = me.GetString(columnName, defaultValue);
        if (resourceKey.HasValue())
        {
            var resourceValue = resourceManager.GetString(resourceKey, CultureInfo.CurrentCulture);
            if (resourceValue.HasValue()) return resourceValue;
            return resourceKey;
        }
        return defaultValue;
    }

    public static byte GetByte(this IDataRecord me, string columnName)
    {
        var i = me.GetColumIndex(columnName, ThrowOnColumnError);
        if (i < 0 || me.IsDBNull(i)) return 0;
        var value = me.GetValue(i);
        if (value is byte a) return a;
        if (value is int c) return (byte)c;
        if (value is double b) return (byte)b;
        throw TypeErrorException(value, columnName);
    }

    public static int GetInt(this IDataRecord me, string columnName, int? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, !defaultValue.HasValue);
        if (i < 0) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        if (me.IsDBNull(i)) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        var value = me.GetValue(i);
        if (value is int b) return b;
        if (value is short a) return a;
        throw TypeErrorException(value, columnName);
    }
    public static int? GetIntOrNull(this IDataRecord me, string columnName, int? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, defaultValue is null);
        if (i < 0) return defaultValue;
        if (me.IsDBNull(i)) return defaultValue;
        var value = me.GetValue(i);
        if (value is int b) return b;
        if (value is short a) return a;
        throw TypeErrorException(value, columnName);
    }

    public static double GetDouble(this IDataRecord me, string columnName, double? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, defaultValue is null);
        if (i < 0) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        if (me.IsDBNull(i)) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        var value = me.GetValue(i);
        if (value is double b) return b;
        if (value is float a) return a;
        throw TypeErrorException(value, columnName);
    }

    public static DateTime GetDate(this IDataRecord me, string columnName)
    {
        var i = me.GetColumIndex(columnName, ThrowOnColumnError);
        return me.GetDateTime(i).Date;
    }

    public static string GetTime(this IDataRecord me, string columnName, string? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, defaultValue is null);
        if (i < 0) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        if (me.IsDBNull(i)) return defaultValue ?? throw TypeErrorException(null, columnName);
        return me.GetDateTime(i).ToString("HH:mm", CultureInfo.InvariantCulture);
    }

    public static TimeSpan GetTimespan(this IDataRecord me, string columnName)
    {
        var i = me.GetColumIndex(columnName, ThrowOnColumnError);
        if (me.IsDBNull(i)) return TimeSpan.MinValue;
        var value = me.GetValue(i);
        if (value is DateTime d) return new TimeSpan(d.Hour, d.Minute, 0);
        throw TypeErrorException(value, columnName);
    }

    public static double GetTimeAsDouble(this IDataRecord me, string columnName)
    {
        var t = me.GetTimespan(columnName);
        return t.TotalMinutes;
    }

    public static bool GetBool(this IDataRecord me, string columnName, bool? defaultValue = null)
    {
        var i = me.GetColumIndex(columnName, defaultValue is null);
        if (i < 0) return defaultValue ?? throw TypeErrorException(defaultValue, columnName);
        if (me.IsDBNull(i)) return false;
        var value = me.GetValue(i);
        if (value is bool a) return a;
        if (value is short b) return b != 0;
        if (value is int c) return c != 0;
        if (value is double d) return d != 0;
        if (defaultValue.HasValue) return defaultValue.Value;
        throw TypeErrorException(value, columnName);
    }

    public static bool IsDBNull(this IDataRecord me, string columnName)
    {
        var i = me.GetOrdinal(columnName);
        return me.IsDBNull(i);
    }

    private static int GetColumIndex(this IDataRecord me, string columnName, bool throwOnNotFound = ThrowOnColumnError)
    {
        int i = -1;
        try { i = me.GetOrdinal(columnName); }
        catch (IndexOutOfRangeException)
        {
            if (throwOnNotFound) throw new InvalidOperationException($"No column '{columnName}' found in data.");
        }
        return i;
    }

    private static Exception TypeErrorException(object? value, string columnName)
    {
        if (value is null) return new InvalidOperationException($"{columnName} has null-value and no default.");
        var type = value.GetType();
        return new InvalidOperationException($"{columnName} has unsupported value type {type.Name}.");
    }
}
