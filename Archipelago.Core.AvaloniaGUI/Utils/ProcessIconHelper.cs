using Avalonia.Media.Imaging;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Archipelago.Core.AvaloniaGUI.Utils
{
    public static class ProcessIconHelper
    {
        public static Bitmap? GetProcessIcon(int pid)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return null;

            try
            {
                using var process = Process.GetProcessById(pid);
                var path = process.MainModule?.FileName;
                if (string.IsNullOrEmpty(path))
                    return null;

                return GetIconFromPath(path);
            }
            catch
            {
                return null;
            }
        }

        [SupportedOSPlatform("windows")]
        private static Bitmap? GetIconFromPath(string exePath)
        {
            try
            {
                using var icon = System.Drawing.Icon.ExtractAssociatedIcon(exePath);
                if (icon == null)
                    return null;

                using var bmp = icon.ToBitmap();
                using var ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                return new Bitmap(ms);
            }
            catch
            {
                return null;
            }
        }
    }
}
