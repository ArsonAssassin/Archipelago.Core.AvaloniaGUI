using ReactiveUI;
using System;

namespace Archipelago.Core.AvaloniaGUI.ViewModels
{
    public class ViewModelBase : ReactiveObject, IDisposable
    {
        public virtual void Dispose()
        {
        }
    }
}
