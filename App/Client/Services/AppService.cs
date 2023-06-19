using System.Reflection;

namespace TimetablePlanning.App.Client.Services;

public static class AppService
    {
        public static string AppVersion
        {
            get
            {
                var version = Version;
                if (version is null) return string.Empty;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
        private static Version? Version => Assembly.GetExecutingAssembly().GetName().Version;
    }
