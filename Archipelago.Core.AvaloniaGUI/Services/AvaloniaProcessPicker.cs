using Archipelago.Core.Helpers;
using Archipelago.Core.AvaloniaGUI.Views;
using Archipelago.Core.Models;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Services
{
    public class AvaloniaProcessPicker : IProcessPicker
    {
        private readonly Window _parentWindow;

        public AvaloniaProcessPicker(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public async Task<ProcessInfo?> PickProcessAsync(string searchText)
        {
            var picker = new ProcessPickerWindow(searchText);
            return await picker.ShowDialog<ProcessInfo?>(_parentWindow);
        }
    }
}
