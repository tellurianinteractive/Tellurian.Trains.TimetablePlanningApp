using TimetablePlanning.Importers.Model;

namespace TimetablePlanning.App.Client.Extensions;

internal static class MessageExtensions
{
    public static string Icon(this Message message) => message.Severity switch
    {
        Severity.Warning => "oi oi-warning",
        Severity.Error => "oi oi-circle-x",
        Severity.Information => "oi oi-check",
        Severity.System => "oi oi-ban",
        _ => string.Empty,
    };

    public static string Color(this Message message) => message.Severity switch
    {
        Severity.Error => "#ff0000",
        Severity.Information => "#339933",
        Severity.Warning => "#ff9900",
        _ => "black"
    };
}
