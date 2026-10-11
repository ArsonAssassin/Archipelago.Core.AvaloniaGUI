using Archipelago.Core.AvaloniaGUI.Models;
using Archipelago.Core.AvaloniaGUI.Utils;
using Archipelago.Core.Models;
using Archipelago.Core.Util.PlatformMemory;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace Archipelago.Core.AvaloniaGUI.ViewModels
{
    public class ProcessPickerViewModel : ViewModelBase
    {
        private string _searchText = string.Empty;
        private ProcessPickerItem? _selectedProcess;
        private ObservableCollection<ProcessPickerItem> _processes = new();

        public ProcessPickerViewModel()
        {
            var hasSelection = this.WhenAnyValue(x => x.SelectedProcess)
                .Select(p => p != null);

            OkCommand = ReactiveCommand.Create(() => { }, hasSelection);
            CancelCommand = ReactiveCommand.Create(() => { });
            RefreshCommand = ReactiveCommand.Create(Refresh);
        }

        public ProcessPickerViewModel(string searchText) : this()
        {
            _searchText = searchText;
            Refresh();
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public ProcessPickerItem? SelectedProcess
        {
            get => _selectedProcess;
            set => this.RaiseAndSetIfChanged(ref _selectedProcess, value);
        }

        public ObservableCollection<ProcessPickerItem> Processes
        {
            get => _processes;
            set => this.RaiseAndSetIfChanged(ref _processes, value);
        }

        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

        public void Refresh()
        {
            Processes.Clear();
            SelectedProcess = null;

            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            var discovered = PlatformMemory.DiscoverProcesses(SearchText);

            foreach (var proc in discovered)
            {
                Processes.Add(new ProcessPickerItem
                {
                    PID = proc.PID,
                    ProcessName = proc.ProcessName,
                    WindowTitle = proc.WindowTitle,
                    Icon = ProcessIconHelper.GetProcessIcon(proc.PID)
                });
            }
        }

        public ProcessInfo? GetSelectedProcessInfo()
        {
            if (SelectedProcess == null)
                return null;

            return new ProcessInfo(
                SelectedProcess.PID,
                SelectedProcess.ProcessName,
                SelectedProcess.WindowTitle
            );
        }
    }
}
