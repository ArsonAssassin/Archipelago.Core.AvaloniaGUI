using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Utils
{
    public static class Helpers
    {
        public static string GetAppVersion()
        {
            var assembly = Assembly.GetEntryAssembly();
            var attribute = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return attribute?.InformationalVersion ?? GetAssemblyVersion();
        }
        public static string GetAssemblyVersion()
        {
            var assemblyVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

            return assemblyVersion ?? "Unknown";
        }
    }
}
