using Archipelago.Core.AvaloniaGUI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Archipelago.Core.AvaloniaGUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        InitializeTitleBarDragging();
        if (!(DataContext is MainWindowViewModel viewModel)) return;
        viewModel.LogList.CollectionChanged += (o, e) => ScrollToEnd(o as ListBox);
        viewModel.HintList.CollectionChanged += (o, e) => ScrollToEnd(o as ListBox);
        viewModel.ItemList.CollectionChanged += (o, e) => ScrollToEnd(o as ListBox);
    }
    private void InitializeTitleBarDragging()
    {
        var titleBar = this.FindControl<Border>("HeaderBar");
        titleBar.PointerPressed += (s, e) =>
        {
            if (e.GetCurrentPoint(titleBar).Properties.IsLeftButtonPressed)
            {
                this.BeginMoveDrag(e);
            }
        };
    }

    public void ScrollToEnd(ListBox listBox)
    {
        if (!(DataContext is MainWindowViewModel viewModel)) return;
        if (viewModel.AutoscrollEnabled)
        {
            if (listBox != null && listBox.ItemCount > 0)
            {
                listBox.ScrollIntoView(listBox.ItemCount - 1);
            }
        }
    }
}
