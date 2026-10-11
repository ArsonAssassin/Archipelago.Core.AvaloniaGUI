using Avalonia.Media.Imaging;

namespace Archipelago.Core.AvaloniaGUI.Models
{
    public class ProcessPickerItem
    {
        public int PID { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public string WindowTitle { get; set; } = string.Empty;
        public Bitmap? Icon { get; set; }
    }
}
