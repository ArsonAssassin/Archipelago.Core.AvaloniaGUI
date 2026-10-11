using System;
using System.Reactive.Linq;
using Archipelago.Core.AvaloniaGUI.ViewModels;
using Archipelago.Core.Models;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Archipelago.Core.AvaloniaGUI.Views;

public partial class ProcessPickerWindow : Window
{
    public ProcessPickerWindow()
    {
        InitializeComponent();
    }

    public ProcessPickerWindow(string searchText)
    {
        InitializeComponent();
        DataContext = new ProcessPickerViewModel(searchText);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        InitializeTitleBarDragging();
        InitializeCommands();
        InitializeDoubleClick();
    }

    private void InitializeTitleBarDragging()
    {
        var titleBar = this.FindControl<Border>("HeaderBar");
        if (titleBar == null) return;

        titleBar.PointerPressed += (s, e) =>
        {
            if (e.GetCurrentPoint(titleBar).Properties.IsLeftButtonPressed)
            {
                this.BeginMoveDrag(e);
            }
        };
    }

    private void InitializeCommands()
    {
        if (DataContext is not ProcessPickerViewModel vm)
            return;

        vm.OkCommand.Subscribe(_ =>
        {
            Close(vm.GetSelectedProcessInfo());
        });

        vm.CancelCommand.Subscribe(_ =>
        {
            Close(null);
        });
    }

    private void InitializeDoubleClick()
    {
        var listBox = this.FindControl<ListBox>("ProcessList");
        if (listBox == null) return;

        listBox.DoubleTapped += (s, e) =>
        {
            if (DataContext is ProcessPickerViewModel vm && vm.SelectedProcess != null)
            {
                Close(vm.GetSelectedProcessInfo());
            }
        };
    }
}
